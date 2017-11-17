using Autofac;
using Ftp.New.BusinessLogic._Interface;
using System;
using System.Configuration;
using System.Framework.Autofac;
using System.IO;
using System.Threading;

namespace FtpNewServices
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
            Console.WriteLine($"配置环境:{Path.GetFileName(path).Substring(0, 3)}");
            string config = File.ReadAllText(path).Replace("\r\n", "");
            var dl = Containers.Resolve<IDownLoadBl>(new NamedParameter("jsonConfig", config));
            //dl.InitializeComponent(@".\private$\FtpDownloadServiceQueue");
            dl.InitializeComponent(@".\private$\FtpDownloadWholeServiceQueue");

            Console.WriteLine("--------------------------------------");
        }
    }
    public class Entit
    {
        public int Num;
    }
}
