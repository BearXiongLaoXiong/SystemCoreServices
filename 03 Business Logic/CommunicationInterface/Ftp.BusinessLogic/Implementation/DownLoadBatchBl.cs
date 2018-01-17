using System;
using System.Collections.Generic;
using System.Framework.Autofac;
using System.Framework.Common;
using System.Framework.Ftp;
using System.Framework.Logging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Ftp.BusinessLogic._Interface;
using Ftp.Entities;

namespace Ftp.BusinessLogic.Implementation
{
    public class DownLoadBatchBl : IDownLoadBl
    {
        private readonly InputDataBase _inputDataBase = new InputDataBase();
        private readonly MsmqHelper _msmq = new MsmqHelper();

        private readonly List<FtpConfig> _ftpConfigs;

        public DownLoadBatchBl(string jsonConfig)
        {
            _ftpConfigs = jsonConfig.FromJson<List<FtpConfig>>();
        }

        public void InitializeComponent(string path)
        {
            //_msmq.InitializeMessageQueue(path);
            //_inputDataBase.InitializeComponent(path);
            StringBuilder stb = new StringBuilder();
            foreach (var config in _ftpConfigs)
            {
                stb.Clear();
                stb.AppendLine($"开始检查 {config.FileType} 配置.....");
                Console.WriteLine();

                if (config.RemoteBackupFolder.Length > 0) stb.AppendLine($"启用文件备份,下载文件将会备份至Ftp/{config.RemoteBackupFolder}目录");
                if (config.NameContains.Length > 0) stb.AppendLine($"启用文件名验证,仅会下载包含{config.NameContains}的文件");
                stb.AppendLine($"{config.FileType} 配置 √");
                Console.WriteLine(stb);
            }


            //Console.WriteLine("running after 3 s...");
            //Thread.Sleep(1000);
            //Console.WriteLine("running after 2 s...");
            //Thread.Sleep(1000);
            //Console.WriteLine("running after 1 s...");
            //Thread.Sleep(1000);
            //Console.WriteLine("Go");


            List<Task> taskList = new List<Task>();
            taskList.AddRange(_ftpConfigs.Select(f => Task.Factory.StartNew(x =>
            {
                f.MaxDegreeOfParallelism = f.MaxDegreeOfParallelism == 0 || f.MaxDegreeOfParallelism > Environment.ProcessorCount ? Environment.ProcessorCount : f.MaxDegreeOfParallelism;
                while (true) BeginWork(f);
            }, 1)));

            taskList.Add(Task.Factory.StartNew(x =>
            {
                while (true) _inputDataBase.BeginWork();
            }, 1));
            Task.WaitAll(taskList.ToArray());
        }

        public void BeginWork(params FtpConfig[] ftpConfigs)
        {
            var ftpConfig = ftpConfigs[0];
            Nlog.Info(ftpConfig.FileType, $"{ftpConfig.FileType}\t开始运行...");

            var ftp = Containers.Resolve<IFtp>(
                new NamedParameter("nLogName", ftpConfig.FileType),
                new NamedParameter("ftpServerIp", ftpConfig.Ip),
                new NamedParameter("ftpProt", ftpConfig.Port),
                new NamedParameter("privateKeyFile", Path.Combine(Environment.CurrentDirectory, @"SFTP_KEY\tmp.cap")),
                new NamedParameter("ftpRemotePath", ftpConfig.RemoteFolder),
                new NamedParameter("ftpUserId", ftpConfig.Uid),
                new NamedParameter("ftpPassword", ftpConfig.Pwd),
                new NamedParameter("isdirectory", ftpConfig.IsDirectory));
            ftp.InitializeFileListStyle();
            //Thread.Sleep(5000);

            var list1 = ftp.FindFiles().Where(x => ftpConfig.NameContains.Length <= 0 || x.Name.ToLower().Contains(ftpConfig.NameContains.ToLower()));
            Thread.Sleep(3000);
            var list2 = ftp.FindFiles();

            var list = list1.Join(list2, x => new { x.Name, x.Size }, y => new { y.Name, y.Size },
                    (x, y) => new FileStruct { IsDirectory = x.IsDirectory, Name = x.Name, CreateTime = x.CreateTime, FullName = x.FullName, Size = x.Size, IsSuccess = false })
                .Where(x => x.IsDirectory == ftpConfig.IsDirectory).ToList(); //&& int.TryParse(x.Size, out int size) && size > 0

            string time = DateTime.Now.ToString("yyyy-MM-dd");
            var messageList = new List<string>();
            Parallel.ForEach(list, new ParallelOptions() { MaxDegreeOfParallelism = ftpConfig.MaxDegreeOfParallelism }, f =>
            {
                f.LocalFullName = $@"{ftpConfig.LocalDirectory}\{ftpConfig.FileType}\{(ftpConfig.LocalBackupFolder.Length > 0 ? ftpConfig.LocalBackupFolder : ftpConfig.RemoteFolder)}\{time}\{Math.Abs(Guid.NewGuid().GetHashCode()).ToString().PadRight(10, '0')}";
                if (ftp.Download(f.Name, f.LocalFullName))
                {
                    f.IsSuccess = true;
                    var message = new SPIN_FLFL_FILE_LOG_INFO_INSERT
                    {
                        pFLFL_TYPE = ftpConfig.FileType,
                        pFILE_NAME = f.Name,
                        pFLFL_URL = $@"{f.LocalFullName}\{f.Name}",
                        pFLFL_STS = "0",
                        pFLFL_USUS_ID = ftpConfig.RemoteFolder
                    }.ToJson();
                    messageList.Add(message);
                }
            });

            //仅全部下载成功情况下,才能整批导入

            if (list.Count > 0 && list.Count(x => !x.IsSuccess) == 0)
            {
                if (_msmq.SendTranMessageQueue(messageList, (result, msg) => Nlog.Info(ftpConfig.FileType, $"{list.Count}个 msmq {(result ? "√" : "failed")} {msg}")))
                    Parallel.ForEach(list, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, f => ftp.Delete(f.FullName));
            }
            else Nlog.Info(ftpConfig.FileType, $"整包下载总数:{list.Count},其中失败{list.Count(x => !x.IsSuccess)}");


            Nlog.Info(ftpConfig.FileType, $"{ftpConfig.FileType}\t运行完毕...\r\n\r\n");
            Thread.Sleep(ftpConfig.ThreadMillisecondsTimeout);
        }
    }
}
