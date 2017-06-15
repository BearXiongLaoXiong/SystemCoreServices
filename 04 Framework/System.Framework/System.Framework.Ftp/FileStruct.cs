using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.Ftp
{
    public class FileStruct
    {
        /// <summary>
        /// 是否为目录
        /// </summary>
        public bool IsDirectory { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 文件或目录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 完整路径(包含文件名或目录名称)
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 本地保存完成路径(包含文件名或目录名称)
        /// </summary>
        public string LocalFullName { get; set; }

        /// <summary>
        /// 操作成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
