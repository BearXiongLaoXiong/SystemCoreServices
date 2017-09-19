using System.Collections.Generic;
using System.Framework.Logging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Framework.Ftp
{
    public class Ftp : IFtp
    {
        #region Private field

        private readonly string _nlogName;

        /// <summary>
        /// FTP服务器地址
        /// </summary>
        private readonly string _ftpUri;

        /// <summary>
        /// FTP服务器IP
        /// </summary>
        private readonly string _ftpServerIp;

        private readonly int _ftpProt;
        private readonly string _privateKeyFile;

        /// <summary>
        /// FTP服务器登录用户名
        /// </summary>
        private readonly string _ftpUserId;

        /// <summary>
        /// FTP服务器登录密码
        /// </summary>
        private readonly string _ftpPassword;

        /// <summary>
        /// 是否目录
        /// </summary>
        private readonly bool _isDirectory;

        /// <summary>
        /// 系统类型
        /// </summary>
        private FileListStyle _fileListStyle = FileListStyle.Unknown;

        #endregion

        #region Constructor Function

        /// <summary>  
        /// 初始化
        /// </summary>
        /// <param name="nLogName"></param>
        /// <param name="ftpServerIp">FTP连接地址</param>
        /// <param name="ftpProt">注入属性</param>
        /// <param name="privateKeyFile">注入属性</param>
        /// <param name="ftpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>  
        /// <param name="ftpUserId">用户名</param>  
        /// <param name="ftpPassword">密码</param>
        /// <param name="isdirectory"></param>  
        public Ftp(string nLogName, string ftpServerIp, int ftpProt, string privateKeyFile, string ftpRemotePath, string ftpUserId, string ftpPassword, bool isdirectory = false)
        {
            _nlogName = nLogName;
            _ftpServerIp = ftpServerIp;
            _ftpProt = ftpProt;
            _privateKeyFile = privateKeyFile;
            _ftpUserId = ftpUserId;
            _ftpPassword = ftpPassword;
            _isDirectory = isdirectory;
            _ftpUri = "ftp://" + ftpServerIp + "/" + ftpRemotePath + "/";
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 建立FTP链接,返回响应对象
        /// </summary>
        /// <param name="uri">FTP地址</param>
        /// <param name="ftpMethod">操作命令</param>
        /// <param name="request"></param>
        /// <returns></returns>
        private FtpWebResponse OpenResponse(Uri uri, string ftpMethod, out FtpWebRequest request)
        {
            request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = ftpMethod;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
            return (FtpWebResponse)request.GetResponse();
        }

        /// <summary>       
        /// 建立FTP链接,返回请求对象       
        /// </summary>      
        /// <param name="uri">FTP地址</param>       
        /// <param name="ftpMethod">操作命令</param>       
        private FtpWebRequest OpenRequest(Uri uri, string ftpMethod)
        {
            var request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = ftpMethod;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
            return request;
        }


        /// <summary>
        /// 从Windows格式中返回文件信息
        /// </summary>
        /// <param name="line">文件信息</param>
        /// <param name="f"></param>
        private void ParseFileStructFromWindowsStyleRecord(string line, ref FileStruct f)
        {
            string processstr = line.Trim();
            string dateStr = processstr.Substring(0, 8);
            processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
            string timeStr = processstr.Substring(0, 7);
            processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();
            DateTimeFormatInfo myDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            myDtfi.ShortTimePattern = "t";
            f.CreateTime = DateTime.Parse(dateStr + " " + timeStr, myDtfi);
            if (processstr.Substring(0, 5) == "<DIR>")
            {
                f.IsDirectory = true;
                processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
            }
            else
            {
                string[] strs = processstr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);   // true);
                processstr = strs[1];
                f.IsDirectory = false;
            }
            f.Name = processstr;
        }


        /// <summary>
        /// 从Unix格式中返回文件信息
        /// </summary>
        /// <param name="line">文件信息</param>
        /// <param name="f"></param>
        private void ParseFileStructFromUnixStyleRecord(string line, ref FileStruct f)
        {
            f.IsDirectory = line.IndexOf("drwxr-xr-x", StringComparison.Ordinal) >= 0 ||
                            line.IndexOf("drwxrwxrwx", StringComparison.Ordinal) >= 0;
            var list = line.Split(' ').ToList();
            list.RemoveAll(x => x.Length == 0);
            if (list.Count > 4) f.Size = list[4];
        }

        #endregion

        /// <summary>
        /// 判断文件列表的方式Window方式还是Unix方式
        /// </summary>
        public void InitializeFileListStyle()
        {
            var listDirectoryDetails = FindFilesAndDirectories(_ftpUri, WebRequestMethods.Ftp.ListDirectoryDetails).FileList;
            string line = listDirectoryDetails.FirstOrDefault(x => x.Length > 8) ?? "";

            if (line.Length > 10 && Regex.IsMatch(line.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
            {
                _fileListStyle = FileListStyle.UnixStyle;
            }
            else if (line.Length > 8 && Regex.IsMatch(line.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
            {
                _fileListStyle = FileListStyle.WindowsStyle;
            }
            else
                _fileListStyle = FileListStyle.Unknown;
        }

        /// <summary>       
        /// 判断指定目录下指定的子目录或文件是否存在       
        /// </summary>   
        /// /// <param name="remoteDirectory">指定的远程目录,如果不存在,则为当前默认目录</param>     
        /// <param name="name">指定的目录或文件名</param>
        /// <returns></returns>
        private bool IsExist(string remoteDirectory, string name)
            => FindFilesByCustomizeDirectory(remoteDirectory).Count(m => m.Name == name) > 0;

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="remoteDirectoryName">目录名</param>
        public bool CreateDirectory(string remoteDirectoryName)
        {
            try
            {
                using (var response = OpenResponse(new Uri(_ftpUri + remoteDirectoryName), WebRequestMethods.Ftp.MakeDirectory, out FtpWebRequest request))
                {
                    request.Abort();
                    return response.StatusCode == FtpStatusCode.FileActionOK;
                }
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"makeDirectory failed,{ex.Message}");
            }
            return false;
        }

        /// <summary>
        /// 更改目录或文件名
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="newName">修改后新名称</param>
        private bool ReName(string uri, string newName)
        {
            try
            {
                var request = OpenRequest(new Uri(uri), WebRequestMethods.Ftp.Rename);
                request.RenameTo = newName;
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    request.Abort();
                    return response.StatusCode == FtpStatusCode.FileActionOK;
                }
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"rename failed,{ex.Message}");
            }
            return false;
        }

        /// <summary>
        /// 更改当前目录下的文件名
        /// </summary>
        /// <param name="currentName">当前名称</param>
        /// <param name="newName">修改后新名称</param>
        /// <returns></returns>
        public bool ReNameByCurrentDirectory(string currentName, string newName)
            => ReName($"{_ftpUri}{currentName}", newName);

        /// <summary>
        /// 更改指定目录下的文件名
        /// </summary>
        /// <param name="remoteBackupDirectory"></param>
        /// <param name="currentFileName"></param>
        /// <returns></returns>
        public bool ReNameToBackupDirectory(string remoteBackupDirectory, string currentFileName)
        {
            if (IsExist(remoteBackupDirectory, currentFileName)) //存在则修改旧文件名
                ReName($"ftp://{_ftpServerIp}/{remoteBackupDirectory}/{currentFileName}", $"{currentFileName}.Backup.{DateTime.Now:yyyyMMddHHmmss}");
            return ReNameByCurrentDirectory(currentFileName, $"/{remoteBackupDirectory}/{currentFileName}");
        }




        private (bool code, List<string> FileList) FindFilesAndDirectories(string uri, string webRequestMethods)
        {
            List<string> listFiles = new List<string>();
            try
            {
                using (var response = OpenResponse(new Uri(uri), webRequestMethods, out FtpWebRequest request))
                {
                    using (var stream = response.GetResponseStream())
                    using (var sr = new StreamReader(stream, Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null) listFiles.Add(line);
                    }
                    request.Abort();
                }
                return (true, listFiles);
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"获取文件信息失败,{uri}.{ex.Message}");
            }
            return (false, listFiles);
        }

        /// <summary>
        /// 获取指定目录下的文件和目录信息
        /// </summary>
        /// <param name="directory">指定的目录</param>
        /// <returns></returns>
        private List<FileStruct> FindFilesByCustomizeDirectory(string directory)
        {
            //必须保留最后的[/]符号,不然给出的列表会是带有directory/fileName这样的结果
            string uri = $"ftp://{_ftpServerIp}/{directory}/";
            return FindFilesAndDirectories(uri, WebRequestMethods.Ftp.ListDirectory).FileList?.Select(x => new FileStruct { Name = x }).ToList();
        }

        /// <summary>
        /// 获取当前目录下指定目录下的文件和一级子目录信息
        /// </summary>
        /// <param name="directory">如未指定目录，则启用默认目录</param>
        /// <returns></returns>
        private (bool code, List<FileStruct> FileList) FindFilesByCurrentDirectories(string directory = "")
        {
            //必须保留最后的[/]符号,不然给出的列表会是带有directory/fileName这样的结果
            string uri = directory.Length == 0 ? _ftpUri : $"{_ftpUri}/{directory}/";
            var res1 = FindFilesAndDirectories(uri, WebRequestMethods.Ftp.ListDirectory);
            var res2 = FindFilesAndDirectories(uri, WebRequestMethods.Ftp.ListDirectoryDetails);


            var fileList = new List<FileStruct>();
            foreach (var name in res1.FileList)
            {
                string line = res2.FileList.FirstOrDefault(x => x.Contains(name)) ?? "";
                if (line.Length > 0)
                {
                    if (_fileListStyle == FileListStyle.Unknown || line == "") continue;
                    FileStruct f = new FileStruct
                    {
                        Name = name,
                        FullName = directory.Length > 0 ? $"{directory}/{name}" : name
                    };
                    switch (_fileListStyle)
                    {
                        case FileListStyle.UnixStyle: ParseFileStructFromUnixStyleRecord(line, ref f); break;
                        case FileListStyle.WindowsStyle: ParseFileStructFromWindowsStyleRecord(line, ref f); break;
                    }
                    fileList.Add(f);
                }
            }
            if (res2.FileList.Count > 0)
            {
                res2.FileList = res2.FileList.Select(x => $"{directory}/{x}").ToList();
                Nlog.Info(_nlogName, $"\r\n{string.Join("\r\n", res2.FileList)}");
            }
            return (res1.code && res2.code, fileList);
        }

        /// <summary>
        /// 获取当前目录下指定目录下的所有文件和所有子目录信息
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private (bool code, List<FileStruct> FileList) FindAllFilesAndDirectories(string directory)
        {
            var files = FindFilesByCurrentDirectories(directory);

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
        /// <param name="directory"></param>
        /// <param name="isContainChildDirectory"></param>
        /// <returns></returns>
        public List<FileStruct> FindFiles(string directory = "", bool isContainChildDirectory = false)
            => isContainChildDirectory ? FindAllFilesAndDirectories(directory).FileList : FindFilesByCurrentDirectories(directory).FileList;




        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="loaclPath">本地文件路径</param>
        public bool Upload(string loaclPath)
        {
            try
            {
                FileInfo fileInf = new FileInfo(loaclPath);
                var request = OpenRequest(new Uri(_ftpUri + fileInf.Name), WebRequestMethods.Ftp.UploadFile);
                request.ContentLength = fileInf.Length;
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                using (var fs = fileInf.OpenRead())
                using (var strm = request.GetRequestStream())
                {
                    var contentLen = fs.Read(buff, 0, buffLength);
                    while (contentLen != 0)
                    {
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }
                }
                request.Abort();
                Nlog.Info(_nlogName, $"{fileInf.Name} \t upload √");
                return true;
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"{Path.GetFileName(loaclPath)} \t upload failed {ex.Message}");
                return false;
            }
        }




        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="remotePath">下载后的保存路径</param>
        /// <param name="localPath">要下载的文件名</param>
        private bool DownloadFile(string remotePath, string localPath)
        {
            //var path = Path.GetDirectoryName(loaclPath);
            //if (path?.Length > 0 && !Directory.Exists(path)) Directory.CreateDirectory(path);
            byte[] buffer = new byte[2048];
            try
            {
                using (var response = OpenResponse(new Uri($"{_ftpUri}/{remotePath}"), WebRequestMethods.Ftp.DownloadFile, out FtpWebRequest request))
                {
                    using (Stream reader = response.GetResponseStream())
                    using (FileStream fileStream = new FileStream(localPath, FileMode.Create))
                        while (true)
                        {
                            int bytesRead = reader.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0) break;
                            fileStream.Write(buffer, 0, bytesRead);
                        }
                    request.Abort();
                }
                var result = File.Exists(localPath) && new FileInfo(localPath).Length > 0;
                Nlog.Info(_nlogName, $"{remotePath} \t down {(result ? "√" : "failed")}");
                return result;
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"{remotePath} \t down failed {ex.Message}");
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
                var fullName = $@"{loaclDirectory}\{f.FullName}";
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
                return DownloadFile(remote, localFile);
            }
            return _isDirectory ? DownloadDirectory(remoteName, localPath) : DownSingleFile(remoteName, localPath);
        }





        /// <summary>  
        /// 删除文件  
        /// </summary>  
        /// <param name="remotePath">要删除的文件</param>
        private bool DeleteFile(string remotePath)
        {
            try
            {
                using (var response = OpenResponse(new Uri($"{_ftpUri}/{remotePath}"), WebRequestMethods.Ftp.DeleteFile, out FtpWebRequest request))
                {
                    request.Abort();
                    return response.StatusCode == FtpStatusCode.FileActionOK;
                }
            }
            catch (Exception ex)
            {
                Nlog.Info(_nlogName, $"{remotePath} delete failed {ex.Message}");
            }
            return false;
        }
        /// <summary>
        /// 删除目录(包括下面所有子目录和子文件)
        /// </summary>
        /// <param name="remoteDirectory">要删除的带路径目录名</param>
        /// <returns></returns>
        private bool DeleteDirectory(string remoteDirectory)
        {
            bool result = true;
            List<FileStruct> allList = FindAllFilesAndDirectories(remoteDirectory).FileList;
            allList.Add(new FileStruct { FullName = remoteDirectory, IsDirectory = true });
            foreach (var f in allList.Where(x => !x.IsDirectory))
            {
                DeleteFile(f.FullName);
            }
            foreach (var f in allList.Where(x => x.IsDirectory).OrderByDescending(x => x.FullName.Count(c => c == '/')))
            {
                try
                {
                    using (OpenResponse(new Uri($"{_ftpUri}/{f.FullName}"), WebRequestMethods.Ftp.RemoveDirectory, out FtpWebRequest request))
                    {
                        request.Abort();
                        Nlog.Info(_nlogName, $"{f.FullName} \t delete √");
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    Nlog.Info(_nlogName, $"{f.FullName} \t delete failed {ex.Message}");
                }
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="remotePath">远程路径</param>
        /// <returns></returns>
        public bool Delete(string remotePath)
            => _isDirectory ? DeleteDirectory(remotePath) : DeleteFile(remotePath);

    }
}
