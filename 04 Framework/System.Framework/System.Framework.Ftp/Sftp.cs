using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.Collections.Generic;
using System.Framework.Logging;
using System.IO;
using System.Linq;

namespace System.Framework.Ftp
{
    public class Sftp : IFtp
    {
        #region Private field
        //private static readonly ConnectionInfo ConnInfo = new ConnectionInfo(
        //    ConfigurationManager.AppSettings["SFTPServer"] ?? "",
        //    int.Parse(ConfigurationManager.AppSettings["SFTPPort"] ?? ""),
        //    ConfigurationManager.AppSettings["SFTPUser"] ?? "",
        //    new AuthenticationMethod[]{
        //        // Pasword based Authentication
        //        new PasswordAuthenticationMethod(ConfigurationManager.AppSettings["SFTPUser"] ?? "",ConfigurationManager.AppSettings["SFTPPwd"] ?? "")
        //        // Key Based Authentication (using keys in OpenSSH Format)
        //        //,new PrivateKeyAuthenticationMethod("username",new PrivateKeyFile[]{new PrivateKeyFile(@"..\openssh.key","passphrase")})
        //        }
        //    );
        private readonly string _nlogName;
        /// <summary>
        /// 是否目录
        /// </summary>
        private readonly bool _isDirectory;

        private readonly ConnectionInfo _connInfo;
        private static string _ftpRemotePath;
        #endregion

        #region Constructor Function
        public Sftp(string nLogName, string ftpServerIp, int ftpProt, string ftpRemotePath, string ftpUserId, string ftpPassword, bool isdirectory = false)
        {
            _nlogName = nLogName;
            _ftpRemotePath = ftpRemotePath;
            _isDirectory = isdirectory;
            _connInfo = new ConnectionInfo(ftpServerIp, ftpProt, ftpUserId,
                new AuthenticationMethod[] {
                    new PasswordAuthenticationMethod(ftpUserId,ftpPassword)
                    //new PrivateKeyAuthenticationMethod("spi", new PrivateKeyFile[] { new PrivateKeyFile(@"C:\Users\laoxiong\Desktop\SFTP_KEY\tmp.cap", "ensurlink123") })
                });

        }

        public Sftp(string nLogName, string ftpServerIp, int ftpProt, string privateKeyFile, string ftpRemotePath, string ftpUserId, string ftpPassword, bool isdirectory = false)
        {
            _nlogName = nLogName;
            _ftpRemotePath = ftpRemotePath;
            _isDirectory = isdirectory;
            _connInfo = new ConnectionInfo(ftpServerIp, ftpProt, ftpUserId,
                new AuthenticationMethod[] {
                    new PrivateKeyAuthenticationMethod(ftpUserId, new PrivateKeyFile(privateKeyFile, ftpPassword))
                });
        }
        #endregion


        public void InitializeFileListStyle()
        {
            //throw new NotImplementedException();
            //var listDirectoryDetails = FindFilesAndDirectories(_ftpUri, WebRequestMethods.Ftp.ListDirectoryDetails);
            //string line = listDirectoryDetails.FirstOrDefault(x => x.Length > 8) ?? "";

            //if (line.Length > 10 && Regex.IsMatch(line.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
            //{
            //    _fileListStyle = FileListStyle.UnixStyle;
            //}
            //else if (line.Length > 8 && Regex.IsMatch(line.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
            //{
            //    _fileListStyle = FileListStyle.WindowsStyle;
            //}
            //else
            //    _fileListStyle = FileListStyle.Unknown;
        }
        public bool ReNameToBackupDirectory(string remoteBackupDirectory, string currentFileName)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 获取当前目录的文件和一级子目录信息
        /// </summary>
        /// <returns></returns>
        private (bool code, List<FileStruct> FileList) ListFilesAndDirectories(string ftpRemotePath)
        {
            ftpRemotePath = ftpRemotePath.Length > 0 ? $"/{_ftpRemotePath}/{ftpRemotePath}" : $"/{_ftpRemotePath}";
            try
            {
                List<SftpFile> sftpFileList;
                using (var sftp = new SftpClient(_connInfo))
                {
                    sftp.Connect();
                    sftpFileList = sftp.ListDirectory(ftpRemotePath)?.Where(x => x.Name != "." && x.Name != "..").ToList();
                    sftp.Disconnect();
                }

                if (sftpFileList?.Count > 0)
                    Nlog.Info(_nlogName, $"\r\n{string.Join("\r\n", sftpFileList.Select(x => $"{x.LastWriteTime} {x.Length.ToString().PadRight(8, ' ')}\t{x.FullName.Replace($"/{_ftpRemotePath}/", "")}"))}");

                if (sftpFileList == null || sftpFileList.Any(x => x.Length == 0))
                {
                    Nlog.Info(_nlogName, $@"获取文件信息失败,{ftpRemotePath.Replace($"/{_ftpRemotePath}/", "")}/{sftpFileList?.FirstOrDefault(x => x.Length == 0)?.Name} size=0||size=null");
                    return (false, new List<FileStruct>());
                }

                return (true, sftpFileList?.Select(x => new FileStruct
                {
                    IsDirectory = x.IsDirectory,
                    CreateTime = x.LastWriteTime,
                    Name = x.Name,
                    FullName = x.FullName,
                    Size = x.Length.ToString()
                }).ToList());
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"获取文件信息失败,{ftpRemotePath.Replace($"/{_ftpRemotePath}/", "")}.{ex.Message}");
            }
            return (false, new List<FileStruct>());

        }
        /// <summary>
        /// 获取当前目录下指定目录下的所有文件和所有子目录信息
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private (bool code, List<FileStruct> FileList) FindAllFilesAndDirectories(string directory)
        {
            var files = ListFilesAndDirectories(directory);

            if (!files.code) return (false, new List<FileStruct>());
            List<FileStruct> list = new List<FileStruct>();
            list.AddRange(files.FileList);
            foreach (var f in files.FileList)
                if (f.IsDirectory)
                {
                    var res = FindAllFilesAndDirectories($"{directory}/{f.Name}");
                    if (!res.code) return (false, new List<FileStruct>());
                    list.AddRange(res.FileList);
                }
            return (true, list);
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="directory">如未指定目录，则启用默认目录</param>
        /// <param name="isContainChildDirectory">true:目录下所有信息(不包含子文件夹);false:目录下所有信息(包含子文件夹)</param>
        /// <returns></returns>
        public List<FileStruct> FindFiles(string directory = "", bool isContainChildDirectory = false)
            => isContainChildDirectory ? FindAllFilesAndDirectories(directory).FileList : ListFilesAndDirectories(directory).FileList;



        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="remoteName">下载后的保存路径</param>
        /// <param name="localPath">要下载的文件名</param>
        private bool DownloadFile(string remoteName, string localPath)
        {
            try
            {
                using (var sftp = new SftpClient(_connInfo))
                {
                    sftp.Connect();
                    var byt = sftp.ReadAllBytes(remoteName);
                    sftp.Disconnect();
                    File.WriteAllBytes(localPath, byt);
                }
                var result = File.Exists(localPath) && new FileInfo(localPath).Length > 0;
                Nlog.Info(_nlogName, $"{remoteName.Replace($"/{_ftpRemotePath}/", "")} \t down {(result ? "√" : "failed")}");
                return result;
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"{remoteName.Replace($"/{_ftpRemotePath}/", "")} \t down failed {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 下载目录
        /// </summary>
        /// <param name="remoteDirectory">FTP文件夹路径</param>
        /// <param name="loaclDirectory">保存的本地文件夹路径</param>
        /// <returns></returns>
        private bool DownloadDirectory(string remoteDirectory, string loaclDirectory)
        {
            bool result = true;
            var res = FindAllFilesAndDirectories(remoteDirectory);

            if (!res.code) return false;
            foreach (var f in res.FileList)
            {
                var fullName = $@"{loaclDirectory}\{f.FullName.Replace(_ftpRemotePath, "")}";
                var directoryName = f.IsDirectory ? fullName : Path.GetDirectoryName(fullName);
                if (directoryName?.Length > 0 && !Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);
                if (f.IsDirectory) continue;
                result &= DownloadFile(f.FullName, fullName);
                if (!result) return false;
            }
            return true;
        }
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
        public bool Download(string remoteName, string localPath)
        {
            bool DownSingleFile(string remote, string local)
            {
                //remote= "201212000001.txt"
                //local = "C:\download\2012-12-12\guid"
                var localFile = Path.Combine(local, remote);
                var localDirectory = Path.GetDirectoryName(localFile);

                try { if (localDirectory?.Length > 0 && !Directory.Exists(localDirectory)) Directory.CreateDirectory(localDirectory); }
                catch
                {
                    // ignored
                }
                return DownloadFile($"/{_ftpRemotePath}/{remote}", localFile);
            }
            return _isDirectory ? DownloadDirectory(remoteName, localPath) : DownSingleFile(remoteName, localPath);
        }



        public bool Upload(string loaclFullName)
        {
            var name = Path.GetFileName(loaclFullName);
            var remotePath = $"{_ftpRemotePath}/{name}";
            using (var client = new SftpClient(_connInfo))
            {
                client.Connect();
                using (var uplfileStream = File.OpenRead(loaclFullName))
                    client.UploadFile(uplfileStream, remotePath, true);
                client.Disconnect();
                return true;
            }
        }



        /// <summary>  
        /// 删除文件  
        /// </summary>  
        /// <param name="remoteName">要删除的文件名</param>
        private bool DeleteFile(string remoteName)
        {
            bool result;
            try
            {
                using (var sftp = new SftpClient(_connInfo))
                {
                    sftp.Connect();
                    sftp.DeleteFile($"/{remoteName}");
                    sftp.Disconnect();
                    result = true;
                }
                Nlog.Info(_nlogName, $"{remoteName.Replace($"/{_ftpRemotePath}/", "")} \t delete √");
            }
            catch (Exception ex)
            {
                result = false;
                Nlog.Info(_nlogName, $"{remoteName.Replace($"/{_ftpRemotePath}/", "")} \t delete failed {ex.Message}");
            }
            return result;
        }
        /// <summary>  
        /// 删除目录 
        /// </summary>  
        /// <param name="directory">要删除的目录名</param>
        private bool DeleteDirectory(string directory)
        {
            bool result;
            try
            {
                using (SshClient ssh = new SshClient(_connInfo))
                {
                    ssh.Connect();
                    using (var scmd = ssh.RunCommand($"rm -rf {directory}"))
                        result = scmd.ExitStatus == 0;
                    ssh.Disconnect();
                }

                Nlog.Info(_nlogName, $"{directory.Replace($"/{_ftpRemotePath}/", "")} \t delete √");
            }
            catch (Exception ex)
            {
                result = false;
                Nlog.Info(_nlogName, $"{directory.Replace($"/{_ftpRemotePath}/", "")} \t delete failed {ex.Message}");
            }
            return result;
        }
        public bool Delete(string remotePath)
        => _isDirectory ? DeleteDirectory(remotePath) : DeleteFile(remotePath);
    }
}
