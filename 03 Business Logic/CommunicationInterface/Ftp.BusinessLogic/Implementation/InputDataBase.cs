using System.Framework.Common;
using System.Framework.Logging;
using System.Threading;
using Ftp.BusinessLogic._Base;
using Ftp.Entities;

namespace Ftp.BusinessLogic.Implementation
{
    public class InputDataBase
    {
        private readonly string _nLogName = "MessageQueue";
        private readonly MsmqHelper _msmq = new MsmqHelper();
        private readonly ICommonBl _commonBl = new CommonBl();

        public void InitializeComponent(string path)
        {
            _msmq.InitializeMessageQueue(path);
        }

        public void BeginWork()
        {
            while (true)
            {
                SPIN_FLFL_FILE_LOG_INFO_INSERT entity = null;
                var result = _msmq.ReceiveTranMessageQueue(x =>
                {
                    entity = x.FromJson<SPIN_FLFL_FILE_LOG_INFO_INSERT>();
                    _commonBl.Execute(entity);
                    return entity.ReturnValue == 0;
                });
                Nlog.Info(_nLogName, $"{entity?.pFILE_NAME}\t{result.msg}");
                if (result.code == 1) break;
            }
            Thread.Sleep(10000);
        }
    }
}
