using System.Collections.Generic;
using System.Messaging;
using System.Linq;

namespace System.Framework.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MsmqHelper
    {
        private string _path = @".\private$\FtpDownloadServiceQueue";
        public void InitializeMessageQueue(string path)
        {
            _path = path;
            if (MessageQueue.Exists(_path)) return;
            Console.WriteLine($"Not found {_path},please create MSMQ");
            throw new Exception($"Not found {_path},please create MSMQ");
        }

        /// <summary>
        /// SendTranMessageQueue
        /// </summary>
        public bool SendTranMessageQueue(string msg, Action<bool, string> action)
        {
            using (MessageQueue queue = new MessageQueue(_path) { Formatter = new XmlMessageFormatter(new[] { typeof(string) }) })
            using (var message = new Message { Label = "FtpInfo", Body = msg })
            using (var tran = new MessageQueueTransaction())
                try
                {
                    tran.Begin();
                    queue.Send(message, tran);
                    tran.Commit();
                    action(true, "");
                    return true;
                }
                catch (Exception ex)
                {
                    action(false, ex.Message);
                    tran.Abort();
                }
            return false;
        }

        public bool SendTranMessageQueue(List<string> list, Action<bool, string> action)
        {
            using (MessageQueue queue = new MessageQueue(_path) { Formatter = new XmlMessageFormatter(new[] { typeof(string) }) })
            using (var tran = new MessageQueueTransaction())
            {
                var messageList = list.Select(x => new Message { Body = x }).ToList();
                try
                {
                    tran.Begin();
                    foreach (var message in messageList) queue.Send(message, tran);
                    tran.Commit();
                    action(true, "");
                    return true;
                }
                catch (Exception ex)
                {
                    action(false, ex.Message);
                    tran.Abort();
                }
                finally
                {
                    foreach (var message in messageList) message?.Dispose();
                }
            }
            return false;
        }

        /// <summary>
        /// ReceiveTranMessageQueue
        /// </summary>
        public (int code, string msg) ReceiveTranMessageQueue(Func<string, bool> func)
        {
            using (MessageQueue queue = new MessageQueue(_path) { Formatter = new XmlMessageFormatter(new[] { typeof(string) }) })
            using (var messageEnumerator = queue.GetMessageEnumerator2())
            {
                if (!messageEnumerator.MoveNext()) return (1, "no msmq data √");
                using (var tran = new MessageQueueTransaction())
                    try
                    {
                        tran.Begin();
                        using (var message = queue.Receive(new TimeSpan(0, 0, 1), tran))
                            if (func(message?.Body.ToString()))
                            {
                                tran.Commit();
                                return (0, "insert sql √");
                            }
                            else
                            {
                                tran.Abort();
                                return (2, $"msmq receive transaction failed");
                            }
                    }
                    catch (Exception ex)
                    {
                        tran.Abort();
                        return (2, $"ReceiveMessage Exception:{ex.Message}");
                    }
            }

            //using (var messageEnumerator = TranMessageQueue.GetMessageEnumerator2())
            //{
            //    while (messageEnumerator.MoveNext())
            //    {
            //        var messageQueueTransaction = new MessageQueueTransaction();
            //        try
            //        {
            //            messageQueueTransaction.Begin();
            //            var message = TranMessageQueue.Receive(new TimeSpan(0, 0, 1), messageQueueTransaction);
            //            if (func(message?.Body.ToString()))
            //                messageQueueTransaction.Commit();
            //            else
            //                messageQueueTransaction.Abort();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine($"ReceiveMessage Exception:{ex.Message}");
            //            messageQueueTransaction.Abort();
            //        }
            //    }
            //}
        }

    }
}
