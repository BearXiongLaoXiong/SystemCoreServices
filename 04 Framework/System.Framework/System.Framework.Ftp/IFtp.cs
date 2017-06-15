using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.Ftp
{
    public interface IFtp
    {
        /// <summary>
        /// 判断文件列表的方式Window方式还是Unix方式
        /// </summary>
        void InitializeFileListStyle();

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="directory">如未指定目录，则启用默认目录</param>
        /// <param name="isContainChildDirectory">true:目录下所有信息(不包含子文件夹);false:目录下所有信息(包含子文件夹)</param>
        /// <returns></returns>
        List<FileStruct> FindFiles(string directory = "", bool isContainChildDirectory = false);

        /// <summary>
        /// 更改指定目录下的文件名
        /// </summary>
        /// <param name="remoteBackupDirectory"></param>
        /// <param name="currentFileName"></param>
        /// <returns></returns>
        bool ReNameToBackupDirectory(string remoteBackupDirectory, string currentFileName);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="loaclFullName">本地文件路径
        /// </param>
        bool Upload(string loaclFullName);

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="remoteName">
        /// <para>isDirectory = true:远程目录:file_img/20170101010000001</para>
        /// <para>isDirectory = false:远程文件:20170101010000001.txt</para>
        /// </param>
        /// <param name="localPath">
        /// <para>isDirectory = true:本地目录:C:\download\2017-01-01\20170101010000001</para>
        /// <para>isDirectory = false:本地文件:C:\download\2017-01-01\20170101010000001</para>
        /// </param>
        /// <returns></returns>
        bool Download(string remoteName, string localPath);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="remotePath">远程路径</param>
        /// <param name="isDirectory"></param>
        /// <returns></returns>
        bool Delete(string remotePath);
    }
}
