using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace HkEbPortal
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">电子邮件的发信人地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="tos">电子邮件的收件人的地址集合</param>
        /// <param name="ccs">电子邮件的抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">此电子邮件的主题行</param>
        /// <param name="context">邮件正文</param>
        /// <param name="fileStreams">附加到此电子邮件的数据的附件集合</param>
        /// <param name="result">错误信息,仅仅在出现异常时有用</param>
        /// <returns></returns>
        public static bool SendSmtpMail(string from, string userName, string passWord, string[] tos, string[] ccs, string subject, string context, Stream[] fileStreams, out string result)
        {
            return SendMail("smtp." + @from.Substring(@from.IndexOf("@", StringComparison.Ordinal) + 1), 25, from, userName, passWord, tos, ccs, subject, context, fileStreams, out result);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">电子邮件的发信人地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="tos">电子邮件的收件人的地址集合</param>
        /// <param name="ccs">电子邮件的抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">此电子邮件的主题行</param>
        /// <param name="context">邮件正文</param>
        /// <param name="files">附加到此电子邮件的数据的附件集合</param>
        /// <param name="result">错误信息,仅仅在出现异常时有用</param>
        /// <returns></returns>
        public static bool SendSmtpMail(string host,string from, string userName, string passWord, string[] tos, string[] ccs, string subject, string context, string[] files, out string result)
        {
            
            return SendMail(host, 25, from, userName, passWord, tos, ccs, subject, context, files, out result);
            //return SendMail("smtp." + @from.Substring(@from.IndexOf("@", StringComparison.Ordinal) + 1), 25, from, userName, passWord, tos, ccs, subject, context, files, out result);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="host">用于SMTP 事务的主机的名称或 IP 地址</param>
        /// <param name="port">用于 SMTP 事务的端口</param>
        /// <param name="from">电子邮件的发信人地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="tos">电子邮件的收件人的地址集合</param>
        /// <param name="ccs">电子邮件的抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">此电子邮件的主题行</param>
        /// <param name="context">邮件正文</param>
        /// <param name="fileStreams">附加到此电子邮件的数据的附件集合</param>
        /// <param name="result">错误信息,仅仅在出现异常时有用</param>
        /// <returns></returns>
        private static bool SendMail(string host, int port, string from, string userName, string passWord, string[] tos, string[] ccs, string subject, string context, Stream[] fileStreams, out string result)
        {
            bool mode;
            result = "";

            MailMessage mailMessage = SetMailMessage(from, tos, ccs, subject, context, fileStreams);
            using (SmtpClient smtpClient = new SmtpClient
            {
                Host = host,
                Port = port,
                Credentials = new NetworkCredential(userName, passWord)
            })
            {
                try
                {
                    //设置邮箱端口，pop3端口:110, smtp端口是:25
                    smtpClient.Send(mailMessage);
                    mode = true;
                }
                catch (Exception err)
                {
                    mode = false;
                    result = err.ToString();
                }
            }
            return mode;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="host">用于SMTP 事务的主机的名称或 IP 地址</param>
        /// <param name="port">用于 SMTP 事务的端口</param>
        /// <param name="from">电子邮件的发信人地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="tos">电子邮件的收件人的地址集合</param>
        /// <param name="ccs">电子邮件的抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">此电子邮件的主题行</param>
        /// <param name="context">邮件正文</param>
        /// <param name="files">附加到此电子邮件的数据的附件集合</param>
        /// <param name="result">错误信息,仅仅在出现异常时有用</param>
        /// <returns></returns>
        private static bool SendMail(string host, int port, string from, string userName, string passWord, string[] tos, string[] ccs, string subject, string context, string[] files, out string result)
        {
            bool mode;
            result = "";

            MailMessage mailMessage = SetMailMessage(from, tos, ccs, subject, context, files);
            using (SmtpClient smtpClient = new SmtpClient
            {
                Host = "202.76.48.38",
                Port = port,
                Credentials = new NetworkCredential(userName, passWord)
            })
            {
                try
                {
                    //设置邮箱端口，pop3端口:110, smtp端口是:25
                    smtpClient.Send(mailMessage);
                    mode = true;
                }
                catch (Exception err)
                {
                    mode = false;
                    result = err.ToString();
                }
            }
            return mode;
        }

        /// <summary>
        /// 设置邮件
        /// </summary>
        /// <param name="from">电子邮件的发信人地址</param>
        /// <param name="tos">电子邮件的收件人的地址集合</param>
        /// <param name="ccs">电子邮件的抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">此电子邮件的主题行</param>
        /// <param name="context">邮件正文</param>
        /// <param name="fileStreams">附加到此电子邮件的数据的附件集合</param>
        /// <returns></returns>
        private static MailMessage SetMailMessage(string from, string[] tos, string[] ccs, string subject, string context, Stream[] fileStreams)
        {
            try
            {
                MailMessage mailMessage = new MailMessage { From = new MailAddress(@from) };

                foreach (string to in tos)
                {
                    mailMessage.To.Add(new MailAddress(to));
                }

                if (ccs != null)
                {
                    foreach (string cc in ccs)
                    {
                        mailMessage.CC.Add(new MailAddress(cc));
                    }
                }
                mailMessage.Subject = subject;
                mailMessage.Body = context;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                if (fileStreams != null)
                {
                    foreach (Stream fileStream in fileStreams)
                    {
                        mailMessage.Attachments.Add(new Attachment(fileStream, ""));
                    }
                }

                return mailMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 设置邮件
        /// </summary>
        /// <param name="from">电子邮件的发信人地址</param>
        /// <param name="tos">电子邮件的收件人的地址集合</param>
        /// <param name="ccs">电子邮件的抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">此电子邮件的主题行</param>
        /// <param name="context">邮件正文</param>
        /// <param name="files">附加到此电子邮件的数据的附件集合</param>
        /// <returns></returns>
        private static MailMessage SetMailMessage(string from, string[] tos, string[] ccs, string subject, string context, string[] files)
        {
            try
            {
                MailMessage mailMessage = new MailMessage { From = new MailAddress(@from) };

                foreach (string to in tos)
                {
                    mailMessage.To.Add(new MailAddress(to));
                }

                if (ccs != null)
                {
                    foreach (string cc in ccs)
                    {
                        mailMessage.CC.Add(new MailAddress(cc));
                    }
                }
                mailMessage.Subject = subject;
                mailMessage.Body = context;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                if (files != null)
                {
                    foreach (string file in files)
                    {
                        if (File.Exists(file))
                        {
                            //using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                            //{
                            Attachment attach = new Attachment(file) { Name = Path.GetFileName(file) };
                            mailMessage.Attachments.Add(attach);
                            // }
                        }
                    }
                }

                return mailMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 将MailMessage保存为eml文件
        /// </summary>
        /// <param name="msg">待保存的具有内容的MailMessage</param>
        /// <param name="emlFileAbsolutePath">保存后的eml文件的路径</param>
        public static void SaveToEml(MailMessage msg, string emlFileAbsolutePath)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            using (MemoryStream ms = new MemoryStream())
            {
                Assembly assembly = typeof(SmtpClient).Assembly;
                Type tMailWriter = assembly.GetType("System.Net.Mail.MailWriter");
                object mailWriter = Activator.CreateInstance(tMailWriter, flags, null, new object[] { ms }, CultureInfo.InvariantCulture);
                msg.GetType().GetMethod("Send", flags).Invoke(msg, new[] { mailWriter, true });
                File.WriteAllText(emlFileAbsolutePath, Encoding.Default.GetString(ms.ToArray()), Encoding.Default);
            }
        }
    }
}