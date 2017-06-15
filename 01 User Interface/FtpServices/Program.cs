using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Framework.Aop;
using System.Framework.Common;
using System.Framework.DataAccess;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Ftp.BusinessLogic.Implementation;
using Ftp.BusinessLogic._Base;
using Ftp.Entities;

namespace FtpServices
{
    class Program
    {

        static void Main(string[] args)
        {
            //var ss = typeof(SPIN_FLFL_FILE_LOG_INFO_INSERT).GetCustomAttributeValue<DatabaseConnectionAttribute>(x => x.ConnectionName);

           

            //var ss1 = typeof(SPIN_FLFL_FILE_LOG_INFO_INSERT).GetCustomAttributeValue<DatabaseConnectionAttribute>(x => x.ConnectionName);
            //Console.ReadLine();
            //CopyFiles();

            string path = $@"{Environment.CurrentDirectory}\{ConfigurationManager.AppSettings["FtpServiceConfig"] ?? ""}";

            if (!File.Exists(path))
            {
                Console.WriteLine($"ftpConfig:{path}");
                Console.WriteLine("请提供Ftp配置文件");
                return;
            }
            Console.WriteLine($"配置环境:{Path.GetFileName(path).Left(3)}");
            string config = File.ReadAllText(path).Replace("\r\n", "");
            DownloadBl dl = new DownloadBl(config);
            dl.InitializeComponent(@".\private$\FtpDownloadServiceQueue");

            //Thread.Sleep(1000);
            var entity = new SPIN_FLFL_FILE_LOG_INFO_INSERT
            {
                pFLFL_STS = "2",
                pFILE_NAME = "testname",
                pFLFL_URL = "testurl",
                pFLFL_TYPE = "testtype",
                pFLFL_USUS_ID = "admin"

            };
            ICommonBl _commonBl = new CommonBl();
            _commonBl.Execute(entity);
            Console.WriteLine("执行第二次");
            _commonBl.Execute(entity);

            MsmqHelper ms = new MsmqHelper();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //Parallel.For(0, 1, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, (i) =>
            //{
            //    ms.ReceiveTranMessageQueue((x) =>
            //    {
            //        Console.WriteLine("paht = " + x);
            //        return true;
            //    });
            //});


            sw.Stop();
            Console.WriteLine(sw.Elapsed);



            Console.WriteLine("--------------------------------------");
            Console.ReadLine();
        }

        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }


        static void CopyFiles()
        {
            var list = new List<string>
            {
                "201720100036928",
                "201720200013119",
                "201720200013123",
                "201720200013125",
                "201720200013126",
                "201720200013127",
                "201720200013128",
                "201720200013130",
                "201720200013133",
                "201720200013386",
                "201720200013398",
                "201720200013472",
                "201720200013488",
                "201720200013500",
                "201720200013507",
                "201720200013513",
                "201720200013514",
                "201720200013519",
                "201720200013537"
            };

            //var files = Directory.EnumerateFiles(@"C:\Users\sh179\Desktop\05-31结案", "*", SearchOption.AllDirectories);

            //foreach (var s in files)
            //{
            //    Console.WriteLine(s);
            //    foreach (var l in list) if (s.Contains(l)) File.Copy(s, $@"C:\Users\sh179\Desktop\新建文件夹\{Path.GetFileName(s)}");
            //}



            var files = Directory.EnumerateFiles(@"C:\Users\sh179\Desktop\1", "*", SearchOption.AllDirectories);
            foreach (var VARIABLE in files)
            {
                Console.WriteLine(VARIABLE);
                File.Copy(VARIABLE, $@"C:\Users\sh179\Desktop\2\{Path.GetFileName(VARIABLE)}");
            }


            //for (int i = 2; i < 500; i++)
            //{
            //    CopyFolder(@"C:\Users\wayne.CPIC-DMZ02\Desktop\新建文件夹\1",@"C:\Users\wayne.CPIC-DMZ02\Desktop\新建文件夹\" + i);
            //    //File.Copy(@"C:\Users\wayne.CPIC-DMZ02\Desktop\新建文件夹\1.txt", $@"C:\Users\wayne.CPIC-DMZ02\Desktop\新建文件夹\{i}.txt");
            //}
            //Console.WriteLine("end-----------------------");
            Console.ReadLine();
        }
    }

    public class Movie
    {

        //public int Genre { get; set; }
    }

    public class TestMoviesInsert
    {
        public string Title1;
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Rating { get; set; }
    }

    #region 文件信息结构
    public struct FileStruct
    {
        public string Flags;
        public string Owner;
        public string Group;
        public bool IsDirectory;
        public DateTime CreateTime;
        public string Name;
    }
    public enum FileListStyle
    {
        UnixStyle,
        WindowsStyle,
        Unknown
    }
    #endregion


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
