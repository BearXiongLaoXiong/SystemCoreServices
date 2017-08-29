using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace System.Framework.Aop
{
    public static class SqlDataRecordExtensions
    {
        public static IEnumerable<SqlDataRecord> ToSqlDataRecord<T>(this IEnumerable<T> entity)
        {
            List<SqlDataRecord> sqlDataRecordList = new List<SqlDataRecord>();

            foreach (var e in entity)
            {
                var customSqlMetaData = SqlMetaDataExtensions.ToSqlMetaData(e).ToList();
                var sqlMetaData = customSqlMetaData.Select(x => x.SqlMetaData).ToArray();
                var sqlDataRecord = new SqlDataRecord(sqlMetaData);

                for (int i = 0; i < sqlMetaData.Length; i++)
                {
                    var value = customSqlMetaData.SingleOrDefault(x => x.Name == sqlMetaData[i].Name)?.Value;
                    if (value != null) sqlDataRecord.SetFloat(i, 1.23F);
                }
                sqlDataRecordList.Add(sqlDataRecord);
            }
            return sqlDataRecordList;

        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++) values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
    public class SqlMetaDataExtensions
    {
        public static IEnumerable<CustomSqlMetaData> ToSqlMetaData<T>(T entity)
        {
            List<CustomSqlMetaData> sqlMetaDataList = new List<CustomSqlMetaData>();
            SqlMetaData sqlMetaData = null;
            PropertyInfo[] propertyInfo = typeof(T).GetProperties();
            foreach (PropertyInfo pi in propertyInfo)
            {
                var attrs = pi.GetCustomAttributes<SqlParameterAttribute>();
                var type = ConvertToDbType(pi.PropertyType);
                var sqlType = ConvertToSqlDbType(pi.PropertyType);
                if (sqlType == SqlDbType.Int || sqlType == SqlDbType.BigInt || sqlType == SqlDbType.Decimal || sqlType == SqlDbType.Float)
                    sqlMetaData = new SqlMetaData(pi.Name, sqlType);
                else
                    sqlMetaData = new SqlMetaData(pi.Name, sqlType,
                        attrs != null && attrs.Any() ? attrs[0].Size : (type == DbType.String ? 50 : 0));

                //SqlParameterAttribute[] attrs = pi.GetCustomAttributes(typeof(SqlParameterAttribute), true) as SqlParameterAttribute[];
                //if (attrs == null || attrs.Length < 1) continue;

                //switch (attrs[0].SqlDbType)
                //{
                //    case SqlDbType.Int: sqlMetaData = new SqlMetaData(pi.Name, attrs[0].SqlDbType); break;
                //    case SqlDbType.VarChar: sqlMetaData = new SqlMetaData(pi.Name, attrs[0].SqlDbType, attrs[0].Size); break;
                //}

                sqlMetaDataList.Add(new CustomSqlMetaData { SqlMetaData = sqlMetaData, Name = pi.Name, Value = pi.GetValue(entity, null) });
            }
            return sqlMetaDataList;

            //PropertyInfo[] propertyInfo = typeof(T).GetProperties();

            //var m = (from pi in propertyInfo
            //        let attrs = pi.GetCustomAttributes(typeof(SqlParameterAttribute), true) as SqlParameterAttribute[]
            //        where attrs != null && attrs.Length >= 1
            //        select new CustomSqlMetaData() { SqlMetaData = new SqlMetaData(pi.Name, attrs[0].SqlDbType, attrs[0].Size), Name = pi.Name, Value = pi.GetValue(entity, null) });
            //return m;
        }

        private static SqlDbType ConvertToSqlDbType(Type type)
        {
            SqlDbType result;
            if (type == typeof(int) || type.IsEnum) result = SqlDbType.Int;
            else if (type == typeof(long)) result = SqlDbType.BigInt;
            else if (type == typeof(double) || type == typeof(Double)) result = SqlDbType.Decimal;
            else if (type == typeof(DateTime)) result = SqlDbType.DateTime;
            else if (type == typeof(bool)) result = SqlDbType.Bit;
            else if (type == typeof(string)) result = SqlDbType.VarChar;
            else if (type == typeof(decimal)) result = SqlDbType.Decimal;
            else if (type == typeof(float)) result = SqlDbType.Float;
            else if (type == typeof(byte[])) result = SqlDbType.Binary;
            else if (type == typeof(Guid)) result = SqlDbType.VarChar;
            else throw new TypeLoadException(nameof(type));
            return result;
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
            else throw new TypeLoadException(nameof(type));
            return result;
        }
    }

    public class CustomSqlMetaData
    {
        public SqlMetaData SqlMetaData { get; set; }

        public string Name { get; set; }

        public object Value { get; set; }
    }



}
