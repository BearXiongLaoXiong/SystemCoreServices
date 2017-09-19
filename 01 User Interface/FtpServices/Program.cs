using Autofac;
using Ftp.BusinessLogic._Interface;
using System;
using System.Configuration;
using System.Framework.Autofac;
using System.Framework.Common;
using System.IO;
using System.Threading;

namespace FtpServices
{
    class Program
    {
        static void Main()
        {
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\{ConfigurationManager.AppSettings["FtpServiceConfig"] ?? ""}";
            if (!File.Exists(path))
            {
                Console.WriteLine($"ftpConfig:{path}");
                Console.WriteLine("请提供Ftp配置文件,5秒后程序将自动退出...");
                Thread.Sleep(5000);
                return;
            }
            Console.WriteLine($"配置环境:{Path.GetFileName(path).Left(3)}");
            string config = File.ReadAllText(path).Replace("\r\n", "");
            var dl = Containers.Resolve<IDownLoadBl>(new NamedParameter("jsonConfig", config));
            //dl.InitializeComponent(@".\private$\FtpDownloadServiceQueue");
            dl.InitializeComponent(@".\private$\FtpDownloadWholeServiceQueue");

            Console.WriteLine("--------------------------------------");
        }

        //public static void CopyFolder(string sourcePath, string destPath)
        //{
        //    if (Directory.Exists(sourcePath))
        //    {
        //        if (!Directory.Exists(destPath))
        //        {
        //            //目标目录不存在则创建
        //            try
        //            {
        //                Directory.CreateDirectory(destPath);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("创建目标目录失败：" + ex.Message);
        //            }
        //        }
        //        //获得源文件下所有文件
        //        List<string> files = new List<string>(Directory.GetFiles(sourcePath));
        //        files.ForEach(c =>
        //        {
        //            string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
        //            File.Copy(c, destFile, true);//覆盖模式
        //        });
        //        //获得源文件下所有目录文件
        //        List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
        //        folders.ForEach(c =>
        //        {
        //            string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
        //            //采用递归的方法实现
        //            CopyFolder(c, destDir);
        //        });
        //    }
        //    else
        //    {
        //        throw new DirectoryNotFoundException("源目录不存在！");
        //    }
        //}
    }


    public class Entit
    {
        public int Num;
    }


    //public class demo
    //{
    //    #region 列出目录文件信息
    //    /// <summary>
    //    /// 列出FTP服务器上面当前目录的所有文件和目录
    //    /// </summary>
    //    public FileStruct[] ListFilesAndDirectories()
    //    {
    //        Response = Open(this.Uri, WebRequestMethods.Ftp.ListDirectoryDetails);
    //        StreamReader stream = new StreamReader(Response.GetResponseStream(), Encoding.Default);
    //        string Datastring = stream.ReadToEnd();
    //        FileStruct[] list = GetList(Datastring);
    //        return list;
    //    }
    //    /// <summary>
    //    /// 列出FTP服务器上面当前目录的所有文件
    //    /// </summary>
    //    public FileStruct[] ListFiles()
    //    {
    //        FileStruct[] listAll = ListFilesAndDirectories();
    //        List<FileStruct> listFile = new List<FileStruct>();
    //        foreach (FileStruct file in listAll)
    //        {
    //            if (!file.IsDirectory)
    //            {
    //                listFile.Add(file);
    //            }
    //        }
    //        return listFile.ToArray();
    //    }

    //    /// <summary>
    //    /// 列出FTP服务器上面当前目录的所有的目录
    //    /// </summary>
    //    public FileStruct[] ListDirectories()
    //    {
    //        FileStruct[] listAll = ListFilesAndDirectories();
    //        List<FileStruct> listDirectory = new List<FileStruct>();
    //        foreach (FileStruct file in listAll)
    //        {
    //            if (file.IsDirectory)
    //            {
    //                listDirectory.Add(file);
    //            }
    //        }
    //        return listDirectory.ToArray();
    //    }
    //    /// <summary>
    //    /// 获得文件和目录列表
    //    /// </summary>
    //    /// <param name="datastring">FTP返回的列表字符信息</param>
    //    private FileStruct[] GetList(string datastring)
    //    {
    //        List<FileStruct> myListArray = new List<FileStruct>();
    //        string[] dataRecords = datastring.Split('\n');
    //        FileListStyle _directoryListStyle = GuessFileListStyle(dataRecords);
    //        foreach (string s in dataRecords)
    //        {
    //            if (_directoryListStyle != FileListStyle.Unknown && s != "")
    //            {
    //                FileStruct f = new FileStruct();
    //                f.Name = "..";
    //                switch (_directoryListStyle)
    //                {
    //                    case FileListStyle.UnixStyle:
    //                        f = ParseFileStructFromUnixStyleRecord(s);
    //                        break;
    //                    case FileListStyle.WindowsStyle:
    //                        f = ParseFileStructFromWindowsStyleRecord(s);
    //                        break;
    //                }
    //                if (!(f.Name == "." || f.Name == ".."))
    //                {
    //                    myListArray.Add(f);
    //                }
    //            }
    //        }
    //        return myListArray.ToArray();
    //    }

    //    /// <summary>
    //    /// 从Windows格式中返回文件信息
    //    /// </summary>
    //    /// <param name="Record">文件信息</param>
    //    private FileStruct ParseFileStructFromWindowsStyleRecord(string Record)
    //    {
    //        FileStruct f = new FileStruct();
    //        string processstr = Record.Trim();
    //        string dateStr = processstr.Substring(0, 8);
    //        processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
    //        string timeStr = processstr.Substring(0, 7);
    //        processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();
    //        DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
    //        myDTFI.ShortTimePattern = "t";
    //        f.CreateTime = DateTime.Parse(dateStr + " " + timeStr, myDTFI);
    //        if (processstr.Substring(0, 5) == "<DIR>")
    //        {
    //            f.IsDirectory = true;
    //            processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
    //        }
    //        else
    //        {
    //            string[] strs = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);   // true);
    //            processstr = strs[1];
    //            f.IsDirectory = false;
    //        }
    //        f.Name = processstr;
    //        return f;
    //    }


    //    /// <summary>
    //    /// 从Unix格式中返回文件信息
    //    /// </summary>
    //    /// <param name="Record">文件信息</param>
    //    private FileStruct ParseFileStructFromUnixStyleRecord(string Record)
    //    {
    //        FileStruct f = new FileStruct();
    //        string processstr = Record.Trim();
    //        f.Flags = processstr.Substring(0, 10);
    //        f.IsDirectory = (f.Flags[0] == 'd');
    //        processstr = (processstr.Substring(11)).Trim();
    //        _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //跳过一部分
    //        f.Owner = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
    //        f.Group = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
    //        _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //跳过一部分
    //        string yearOrTime = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
    //        if (yearOrTime.IndexOf(":") >= 0)  //time
    //        {
    //            processstr = processstr.Replace(yearOrTime, DateTime.Now.Year.ToString());
    //        }
    //        f.CreateTime = DateTime.Parse(_cutSubstringFromStringWithTrim(ref processstr, ' ', 8));
    //        f.Name = processstr;   //最后就是名称
    //        return f;
    //    }

    //    /// <summary>
    //    /// 按照一定的规则进行字符串截取
    //    /// </summary>
    //    /// <param name="s">截取的字符串</param>
    //    /// <param name="c">查找的字符</param>
    //    /// <param name="startIndex">查找的位置</param>
    //    private string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
    //    {
    //        int pos1 = s.IndexOf(c, startIndex);
    //        string retString = s.Substring(0, pos1);
    //        s = (s.Substring(pos1)).Trim();
    //        return retString;
    //    }

    //    /// <summary>
    //    /// 判断文件列表的方式Window方式还是Unix方式
    //    /// </summary>
    //    /// <param name="recordList">文件信息列表</param>
    //    private FileListStyle GuessFileListStyle(string[] recordList)
    //    {
    //        foreach (string s in recordList)
    //        {
    //            if (s.Length > 10 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
    //            {
    //                return FileListStyle.UnixStyle;
    //            }
    //            if (s.Length > 8 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
    //            {
    //                return FileListStyle.WindowsStyle;
    //            }
    //        }
    //        return FileListStyle.Unknown;
    //    }
    //    #endregion
    //}
}
