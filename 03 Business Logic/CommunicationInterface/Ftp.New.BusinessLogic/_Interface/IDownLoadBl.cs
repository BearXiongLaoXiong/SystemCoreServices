using System;
using System.Collections.Generic;
using System.Framework.Ftp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ftp.New.BusinessLogic._Interface
{
    public interface IDownLoadBl
    {
        void InitializeComponent(string path);
        void BeginWork(params FtpConfig[] ftpConfigs);
    }
}
