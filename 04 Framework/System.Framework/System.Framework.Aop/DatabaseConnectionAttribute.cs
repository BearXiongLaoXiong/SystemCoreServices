namespace System.Framework.Aop
{
    /// <summary>
    /// 数据库连接名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseConnectionAttribute : System.Attribute
    {
        public string ConnectionName { get; }
        public string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="type">在ConnectionEnum.CustomizeConnectionString条件下必填type</param>
        public DatabaseConnectionAttribute(ConnectionEnum connectionName, Type type = null)
        {
            if (connectionName == ConnectionEnum.CustomizeConnectionString)
                if (type == null) throw new ArgumentException($"DatabaseConnectionAttribute.{nameof(type)}不允许为空");
                else if (!typeof(ICustomizeConnectionString).IsAssignableFrom(type)) throw new ArgumentException($"{type.Name}必须实现接口{nameof(ICustomizeConnectionString)}");
            ConnectionName = connectionName.ToString();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="connectionString">例:Data Source=192.168.x.x,uid=sa;pwd=xxx;...</param>
        //public DatabaseConnectionAttribute(string connectionString)
        //{
        //    ConnectionName = nameof(ConnectionEnum.CustomizeConnectionString);
        //    ConnectionString = connectionString;
        //}
        //TypeDescriptor.AddAttributes(typeof(TestBll), new DatabaseConnectionAttribute("1"));
    }

    /// <summary>
    /// 库名
    /// </summary>
    public enum ConnectionEnum
    {
        /// <summary>
        /// 自定义连接语句,例:Data Source=192.168.x.x,uid=sa;pwd=xxx;...,注:仅为标识,不可在构造方法中调用此类型
        /// </summary>
        CustomizeConnectionString,
        /// <summary>
        /// 默认库可以不标记Attribute(因prod sp 最多,所以定为prod库)
        /// </summary>
        DefaultConnectionString,
        /// <summary>
        /// 初审.user库
        /// </summary>
        User,
        /// <summary>
        /// 初审.intfs库
        /// </summary>
        Intfs,

        /// <summary>
        ///  码表库
        /// </summary>
        CodeProd,

        DwProd

    }

    /// <summary>
    /// 连接字符串
    /// </summary>
    public interface ICustomizeConnectionString
    {
        /// <summary>
        /// 例:Data Source=192.168.x.x,uid=sa;pwd=xxx;...
        /// </summary>
        string ConnectionString { get; set; }
    }
}
