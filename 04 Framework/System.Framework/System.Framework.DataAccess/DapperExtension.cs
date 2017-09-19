using Dapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Framework.Aop;
using System.Linq;
using System.Reflection;
using Microsoft.SqlServer.Server;

namespace System.Framework.DataAccess
{
    public class DapperExtension<T> where T : class
    {
        private static readonly Dictionary<string, string> ConnectionStringCache = new Dictionary<string, string>();
        private static IDbConnection CreateDbConnection(T t)
        {
            var connectionName = typeof(T).GetCustomAttributeValue<DatabaseConnectionAttribute>(x => x.ConnectionName);
            switch (connectionName ?? "")
            {
                case nameof(ConnectionEnum.CustomizeConnectionString):
                    return t is ICustomizeConnectionString con && con.ConnectionString.Length > 0 ? new SqlConnection(con.ConnectionString) : throw new ArgumentNullException(nameof(ICustomizeConnectionString.ConnectionString));
                //return t is ICustomizeConnectionString con
                //    ? new SqlConnection(con.ConnectionString)
                //    : new SqlConnection(TypeDescriptor.GetAttributes(typeof(T)).OfType<DatabaseConnectionAttribute>().FirstOrDefault()?.ConnectionString ?? "");
                //return new SqlConnection(typeof(T).GetCustomAttributes<DatabaseConnectionAttribute>().ConnectionString);
                //return new SqlConnection(typeof(T).GetProperty(nameof(ICustomizeConnectionString.ConnectionString))?.GetValue(t).ToString() ?? "");
                default:
                    if (string.IsNullOrEmpty(connectionName)) connectionName = nameof(ConnectionEnum.DefaultConnectionString);
                    if (!ConnectionStringCache.ContainsKey(connectionName))
                    {
                        //Console.WriteLine("sql取配置");
                        lock (connectionName + "ConnectionString")
                        {
                            if (!ConnectionStringCache.ContainsKey(connectionName))
                            {
                                var connectionString = ConfigurationManager.AppSettings[connectionName] ?? "";
                                ConnectionStringCache[connectionName] = connectionString;
                            }
                        }
                        if (ConnectionStringCache[connectionName] == null || ConnectionStringCache[connectionName].Length < 1) throw new ArgumentNullException(nameof(ConfigurationManager.ConnectionStrings));
                    }
                    break;
            }

            //Console.WriteLine("sql取缓存");
            return new SqlConnection(ConnectionStringCache[connectionName]);
        }
        private static DbType ConvertToDbType(Type type)
        {
            DbType result;
            if (type == typeof(int) || type.IsEnum) result = DbType.Int32;
            else if (type == typeof(long)) result = DbType.Int32;
            else if (type == typeof(double) || type == typeof(Double)) result = DbType.Decimal;
            else if (type == typeof(DateTime)) result = DbType.DateTime;
            else if (type == typeof(bool)) result = DbType.Boolean;
            else if (type == typeof(string)) result = DbType.String;
            else if (type == typeof(decimal)) result = DbType.Decimal;
            else if (type == typeof(float)) result = DbType.Single;
            else if (type == typeof(byte[])) result = DbType.Binary;
            else if (type == typeof(Guid)) result = DbType.Guid;
            else if (type == typeof(List<SqlDataRecord>)) result = DbType.Object;
            else if (type == typeof(DataTable)) result = DbType.Object;
            else throw new TypeLoadException(nameof(type));
            return result;
        }

        private static DynamicParameters GetParameter(T entity)
        {
            var parameters = new DynamicParameters();

            var propertyInfo = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo pi in propertyInfo)
            {
                var attrs = pi.GetCustomAttributes<SqlParameterAttribute>();
                var type = ConvertToDbType(pi.PropertyType);

                if (pi.PropertyType == typeof(DataTable))
                    parameters.AddDynamicParams(new Dictionary<string, object> { { pi.Name, (pi.GetValue(entity, null) as DataTable).AsTableValuedParameter() } });
                else if (pi.Name != nameof(ICustomizeConnectionString.ConnectionString))
                    parameters.Add(pi.Name, pi.GetValue(entity, null), type,
                        attrs != null && attrs.Any() ? attrs[0].Direction : ParameterDirection.Input,
                        attrs != null && attrs.Any() ? attrs[0].Size : (type == DbType.String ? 50 : 0));
            }
            return parameters;
        }

        private static void SetParameter(DynamicParameters parameters, T entity)
        {
            var propertyInfo = typeof(T).GetProperties();
            foreach (PropertyInfo pi in propertyInfo)
            {
                var attrs = pi.GetCustomAttributes<SqlParameterAttribute>();
                if (attrs == null || attrs.Length < 1) continue;
                if (attrs[0].Direction != ParameterDirection.Output && attrs[0].Direction != ParameterDirection.ReturnValue) continue;
                var type = pi.PropertyType;
                if (type == typeof(int) || type.IsEnum) pi.SetValue(entity, parameters.Get<int>(pi.Name), null);
                else if (type == typeof(string)) pi.SetValue(entity, parameters.Get<string>(pi.Name), null);
                else if (type == typeof(DateTime)) pi.SetValue(entity, parameters.Get<DateTime>(pi.Name), null);
                else if (type == typeof(long)) pi.SetValue(entity, parameters.Get<long>(pi.Name), null);
                else if (type == typeof(double) || type == typeof(Double)) pi.SetValue(entity, parameters.Get<double>(pi.Name), null);
                else if (type == typeof(bool)) pi.SetValue(entity, parameters.Get<bool>(pi.Name), null);
                else if (type == typeof(decimal)) pi.SetValue(entity, parameters.Get<decimal>(pi.Name), null);
                else if (type == typeof(float)) pi.SetValue(entity, parameters.Get<float>(pi.Name), null);
                else if (type == typeof(byte[])) pi.SetValue(entity, parameters.Get<byte[]>(pi.Name), null);
                else if (type == typeof(Guid)) pi.SetValue(entity, parameters.Get<Guid>(pi.Name), null);
                else throw new TypeLoadException(nameof(type));
            }
        }

        public static void Execute(T entity)
        {
            var parameters = GetParameter(entity);
            using (IDbConnection cnn = CreateDbConnection(entity))
            {
                var data = cnn.Execute(typeof(T).Name, parameters, null, null, CommandType.StoredProcedure);
                SetParameter(parameters, entity);
            }
        }

        public static List<TFirst> QuerySingle<TFirst>(T entity) where TFirst : class
        {
            var parameters = GetParameter(entity);
            List<TFirst> listFirst;
            using (IDbConnection cnn = CreateDbConnection(entity))
            {
                listFirst = cnn.Query<TFirst>(typeof(T).Name, parameters, null, false, null, CommandType.StoredProcedure)?.ToList();
                SetParameter(parameters, entity);
            }
            return listFirst;
        }

        public static (List<TFirst> ListFirst, List<TSecond> ListSecond) QueryMultiple<TFirst, TSecond>(T entity) where TFirst : class
        {
            var parameters = GetParameter(entity);
            List<TFirst> listFirst;
            List<TSecond> listSecond;
            using (IDbConnection cnn = CreateDbConnection(entity))
            {
                var data = cnn.QueryMultiple("TestMoviesUpdate", parameters, null, null, CommandType.StoredProcedure);
                listFirst = data.Read<TFirst>().ToList();
                listSecond = data.Read<TSecond>().ToList();
                SetParameter(parameters, entity);
            }
            return (listFirst, listSecond);
        }

        public static (List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird) QueryMultiple<TFirst, TSecond, TThird>(T entity)
            where TFirst : class
            where TSecond : class
            where TThird : class
        {
            var parameters = GetParameter(entity);
            List<TFirst> listFirst;
            List<TSecond> listSecond;
            List<TThird> listThird;
            using (IDbConnection cnn = CreateDbConnection(entity))
            {
                var data = cnn.QueryMultiple("TestMoviesUpdate", parameters, null, null, CommandType.StoredProcedure);
                listFirst = data.Read<TFirst>().ToList();
                listSecond = data.Read<TSecond>().ToList();
                listThird = data.Read<TThird>().ToList();
                SetParameter(parameters, entity);
            }
            return (listFirst, listSecond, listThird);
        }

        public static DataSet ExecuteDataSet(T entity)
        {
            DataSet ds = new XDataSet();
            var parameters = GetParameter(entity);
            using (IDbConnection cnn = CreateDbConnection(entity))
            {
                IDataReader reader = cnn.ExecuteReader("TestMoviesUpdate", parameters, null, null, CommandType.StoredProcedure);
                SetParameter(parameters, entity);
                ds.Load(reader, LoadOption.OverwriteChanges, null, new DataTable[] { });
            }
            return ds;
        }
    }

    internal sealed class XLoadAdapter : DataAdapter
    {
        public int FillFromReader(DataSet ds, IDataReader dataReader, int startRecord, int maxRecords)
        {
            return Fill(ds, "Table", dataReader, startRecord, maxRecords);
        }
    }

    internal sealed class XDataSet : DataSet
    {
        public override void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler handler, params DataTable[] tables)
        {
            XLoadAdapter adapter = new XLoadAdapter
            {
                FillLoadOption = loadOption,
                MissingSchemaAction = MissingSchemaAction.AddWithKey
            };
            if (handler != null)
            {
                adapter.FillError += handler;
            }
            adapter.FillFromReader(this, reader, 0, 0);
            if (!reader.IsClosed && !reader.NextResult())
            {
                reader.Close();
            }
        }
    }
}
