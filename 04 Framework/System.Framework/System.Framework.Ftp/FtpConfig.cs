using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.Ftp
{
    public class Configs
    {
        public List<FtpConfig> DownloadConfigs { get; set; }
    }

    public class FtpConfig
    {
        /// <summary>
        /// 下载类型
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// 例:192.168.1.1
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 例:21
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 例:having.xiong
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 例:1qaz2wsx
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// ftp 存放目录(如果有,则文件夹名称,app/ps)
        /// </summary>
        public string RemoteFolder { get; set; }

        /// <summary>
        /// ftp 备份存放目录(如果有,则文件夹名称,app/ps)
        /// </summary>
        public string RemoteBackupFolder { get; set; }

        /// <summary>
        /// 私有队列地址
        /// </summary>
        public string MessageQueuePath { get; set; }

        /// <summary>
        /// 本地保存文件路径 例:D:\保存路径\我的文档
        /// </summary>
        public string LocalDirectory { get; set; }
        /// <summary>
        /// 本地保存文件目录 例:图片文件,(注:如未填写本属性则取RemoteFolder值为目录名)
        /// </summary>
        public string LocalBackupFolder { get; set; }

        /// <summary>
        /// 文件名包含字符(做验证用)
        /// </summary>
        public string NameContains { get; set; }

        /// <summary>
        /// 是否下载目录:true:仅仅下载目录(包含子目录,子文件),false:仅仅下载文件
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// 循环等待时间
        /// </summary>
        public int ThreadMillisecondsTimeout { get; set; }

        /// <summary>
        /// 实例所允许的并发任务的最大数目，如超过最大核心数量,将默认为最大核心数量
        /// </summary>
        public int MaxDegreeOfParallelism { get; set; }
    }
}
