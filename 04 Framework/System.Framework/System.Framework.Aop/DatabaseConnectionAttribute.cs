namespace System.Framework.Aop
{
    /// <summary>
    /// 数据库连接名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseConnectionAttribute : System.Attribute
    {
        public string ConnectionName { get; }
        public DatabaseConnectionAttribute(ConnectionEnum connectionName)
        {
            ConnectionName = connectionName.ToString();
        }
    }

    /// <summary>
    /// 库名
    /// </summary>
    public enum ConnectionEnum
    {
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
}
