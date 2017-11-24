using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using HkEbPortal.Models.EB_PORTAL;
using System.IO;
using System.Net;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using HkEbPortal.Filters;

namespace HkEbPortal.Controllers
{
    public class UserController : BaseController
    {

        private readonly string _from = ConfigurationManager.AppSettings["EmailFrom"];
        private readonly string _userName = ConfigurationManager.AppSettings["EmailUsername"];
        private readonly string _host = ConfigurationManager.AppSettings["EmailHost"];
        private readonly string _passWord = ConfigurationManager.AppSettings["EmailPassword"];


        public ActionResult Login()
        {
            //Debug.WriteLine("=====================referrer = "+HttpContext.Request.UrlReferrer?.ToString());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(string txtpolicyNo, string txtMember, string txtPassword)
        {
            var entity = new SPEH_FMFM_LOGIN
            {
                pPOLICYNO = txtpolicyNo,
                pUSUS_ID = txtMember,
                pUSUS_PSWD = Des.Encrypt(txtPassword)
            };
            var userInfo = CommonBl.QuerySingle<SPEH_FMFM_LOGIN, UserInfo>(entity).FirstOrDefault();

            //保單號碼, 會員編號或密碼不正確
            if (string.IsNullOrWhiteSpace(userInfo?.USUS_ID))
                return Json(new { Code = entity.ReturnValue, Msg = "" }, JsonRequestBehavior.DenyGet);

            if (userInfo?.USUS_EMAIL?.Length == 0)
                return Json(new { Code = 2, Msg = "Do not find your mailbox, please go to the registration interface to activate the mailbox!" }, JsonRequestBehavior.DenyGet);

            if (userInfo?.USUS_EMAIL_ISACTIVE == "0")
                return Json(new { Code = 3, Msg = userInfo.USUS_EMAIL }, JsonRequestBehavior.DenyGet);


            Session.RemoveAll();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                userInfo.USUS_KY,
                DateTime.Now,
                DateTime.Now.AddHours(12),
                false,//將管理者登入的 Cookie 設定成 Session Cookie
                userInfo.NAME,//userdata看你想存放啥
                FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);
            //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Path = "/", Expires = DateTime.Now.AddHours(1), HttpOnly = true, Secure = true });
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            Session[FormsAuthentication.FormsCookieName] = userInfo;


            var memberList = new List<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0>();
            if (userInfo.USUS_FIRST_ISACTIVE == "0")
            {
                var list = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB
                {
                    pEHUSER = UserInfo.USUS_ID
                };
                var result = CommonBl.QueryMultiple<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT1, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT2>(list);
                memberList = result.ListFirst;
            }

            return Json(new { Code = 1, Msg = "", Data = new { userInfo.NAME, userInfo.GPGP_NAME, userInfo.USUS_FIRST_ISACTIVE, userInfo.USUS_INFO_IS_CONFIRM, MemberList = memberList } }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SignUp(string txtPolicyUp, string txtMemberUp, string txtBirthday, string txtEmailUp = "", string confirmEmail = "")
        {
            var entity = new SPEH_USUS_EMAIL_SELECT_ISACTIVE
            {
                pPolicy_NO = txtPolicyUp,
                pCert_No = txtMemberUp
            };
            var userInfo = CommonBl.QuerySingle<SPEH_USUS_EMAIL_SELECT_ISACTIVE, SPEH_USUS_EMAIL_ISACTIVE_RESULT>(entity).FirstOrDefault();

            //没有在HK_EB_DATE中找到对应的数据,保單號碼, 會員編號或密碼不正確
            if (userInfo == null)
                return Json(new { Code = 1, Msg = "" }, JsonRequestBehavior.DenyGet);

            //在HK_EB_DATE中找到了对应的数据 且ususID已注册 且email已注册过
            if (userInfo.USUS_ID.Length > 0 && userInfo.USUS_SIGNUP_ISACTIVE.Equals("1"))
                return Json(new { Code = 2, Msg = "" }, JsonRequestBehavior.DenyGet);

            //出身日期不一致，直接视为没找到账号
            DateTime.TryParse(txtBirthday, out DateTime dob);
            if (string.IsNullOrWhiteSpace(userInfo.DOB) || !userInfo.DOB.Equals(dob.ToString("yyyy-MM-dd").Trim()))
                return Json(new { Code = 1, Msg = "" }, JsonRequestBehavior.DenyGet);

            txtEmailUp = string.IsNullOrEmpty(txtEmailUp) ? userInfo.USUS_EMAIL : txtEmailUp;

            //請輸入email
            if (string.IsNullOrWhiteSpace(userInfo.USUS_EMAIL) && string.IsNullOrWhiteSpace(txtEmailUp))
                return Json(new { Code = 3, Msg = "" }, JsonRequestBehavior.DenyGet);

            if (!string.IsNullOrWhiteSpace(userInfo.USUS_EMAIL) && userInfo.USUS_EMAIL_ISACTIVE == "0")
                if (confirmEmail == "confirm")
                {
                    var emialConfirm = new SPEH_USUS_EMAIL_ISACTIVE_UPDATE
                    {
                        pPolicy_NO = txtPolicyUp,
                        pCert_No = txtMemberUp
                    };
                    CommonBl.Execute(emialConfirm);
                }
                else return Json(new { Code = 4, Msg = userInfo.USUS_EMAIL }, JsonRequestBehavior.DenyGet);


            //在HK_EB_DATE中找到了对应的数据 但userId未注册
            if (userInfo.USUS_ID.Length > 0)
            {
                string password = Des.GetDesStr();
                var insert = new SPEH_USUS_EMAIL_INSERT
                {
                    pPolicy_NO = txtPolicyUp,
                    pCert_No = txtMemberUp,
                    pPassword = Des.Encrypt(password),
                    pEmail = txtEmailUp
                };
                CommonBl.Execute(insert);

                if (insert.ReturnValue == 1)
                {
                    string subject = "We Care – Flexi Portal – Password";
                    var context = new StringBuilder();
                    context.Append("<div style='font-family: Arial;font-size:14px;line-height:30px;'>");
                    context.Append("Dear Sir/ Madam, </br>");
                    context.Append("Thank you for registering our Flexible Benefit Portal, below is your new password: </br>");
                    context.AppendFormat("Password: {0} </br>", password);
                    context.Append("It is highly recommended to change your password immediately and periodically.</br>");
                    context.Append("If you have questions regarding this portal, please feel free to contact our Member Services Hotline at(852) 3187 6831 or send email to <a href='medicalcs@generali.com.hk'>medicalcs@generali.com.hk</a> </br>");
                    context.Append("(Please do not send email to us by replying this auto - email.) </br>");
                    context.Append("Best regards, </br>");
                    context.Append("Assicurazioni Generali S.p.A. – Hong Kong Branch </br>");
                    context.Append("</div>");
                    SendEmail(subject, context.ToString(), txtEmailUp);
                }

                return Json(new { Code = 0, Msg = insert.ReturnValue == 1 ? "success" : "failed" }, JsonRequestBehavior.DenyGet);
            }

            return Json(new { Code = "999", Msg = "error!" });
        }

        private void SendEmail(string subject, string context, string txtEmailUp)
        {
            EmailHelper.SendSmtpMail(_host, _from, _userName, _passWord, new[] { txtEmailUp }, new string[] { },
                                    subject,
                                    context,
                                    new string[] { }, out string result);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Clear();
            return Redirect("../eflexi/User/Login");
        }


        [Authorization]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorization]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(FormCollection form)
        {
            string oldpwd = form["oldPassword"];
            string newpwd = form["newPassword"];
            string confirmpwd = form["confirmPassword"];
            var entity = new SPEH_USUS_USER_PWD_INFO_UPDATE
            {
                pUSUS_ID = UserInfo.USUS_ID,
                pPassword = Des.Encrypt(oldpwd),
                pConfirmPassword = Des.Encrypt(confirmpwd)
            };
            CommonBl.Execute(entity);

            return Json(new { Code = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.DenyGet);
        }



        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ForgotPassword(string policyNo, string memberId)
        {
            string password = Des.GetDesStr();
            var entity = new SPEH_USUS_USER_PWD_INFO_SELECT { pPLPL_NO = policyNo, pMEME_ID = memberId, pPassword = Des.Encrypt(password) };
            var list = CommonBl.QuerySingle<SPEH_USUS_USER_PWD_INFO_SELECT, SPEH_USUS_USER_PWD_INFO_SELECT_RESULT>(entity);
            if (list.Count > 0 && entity.pRTN_CD == 0)
            {
                string subject = "We Care – Flexi Portal – Password Reset";
                var context = new StringBuilder();
                context.Append("<div style='font-family: Arial;font-size:14px;line-height:30px;'>");
                context.Append("Dear Sir/ Madam, </br>");
                context.Append("Your password has been reset.  Below is your new password: </br>");
                context.AppendFormat("Password: {0} </br>", password);
                context.Append("It is highly recommended to change the password immediately and periodically.</br>");
                context.Append("If you have questions regarding this portal, please feel free to contact our Member Services Hotline at(852) 3187 6831 or send email to <a href='medicalcs@generali.com.hk'>medicalcs@generali.com.hk</a> </br>");
                context.Append("(Please do not send email to us by replying this auto - email.) </br>");
                context.Append("Best regards, </br>");
                context.Append("Assicurazioni Generali S.p.A. – Hong Kong Branch </br>");
                context.Append("</div>");
                SendEmail(subject, context.ToString(), list.FirstOrDefault()?.USUS_EMAIL);
            }
            return Json(new { Data = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.AllowGet);
        }







        /* //转pdf
        [HttpPost]
        public JsonResult HTMLConvertPDF()
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/UploadPdf");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            WebClient wc = new WebClient();
            // 从网上下载html字符串
            wc.Encoding = System.Text.Encoding.UTF8;
            string htmlText = getWebContent();

            byte[] pdfFile = this.ConvertHtmlTextToPDF(htmlText);

            string fileId = "/file_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            System.IO.File.WriteAllBytes(path + fileId, pdfFile);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取网站内容，包含了 HTML+CSS+JS
        /// </summary>
        /// <returns>String返回网页信息</returns>
        public string getWebContent()
        {
            try
            {
                string INPATH = System.Web.HttpContext.Current.Server.MapPath("~/bin/Index.html");
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                //获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData(INPATH);
                Byte[] pageData2 = System.Text.Encoding.UTF8.GetBytes("<html><style>body{font-family:pingfang sc light;}</style><body><table><tr><td style='color:red;'>sadsdb阿萨德</td></tr></table>第一页1p开始<p style='font-size:28px;color:yellow;'>傻大个</p></body></html>");
                //从指定网站下载数据
                string pageHtml = System.Text.Encoding.UTF8.GetString(pageData2);
                //如果获取网站页面采用的是GB2312，则使用这句       
                bool isBool = isMessyCode(pageHtml);//判断使用哪种编码 读取网页信息
                if (!isBool)
                {
                    string pageHtml1 = System.Text.Encoding.UTF8.GetString(pageData2);
                    pageHtml = pageHtml1;
                }
                else
                {
                    string pageHtml2 = System.Text.Encoding.Default.GetString(pageData2);
                    pageHtml = pageHtml2;
                }
                return pageHtml;
            }

            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
                return webEx.Message;
            }
        }
        /// <summary>
        /// 判断是否有乱码
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public bool isMessyCode(string txt)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(txt);            //239 191 189            
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < bytes.Length - 3)
                    if (bytes[i] == 239 && bytes[i + 1] == 191 && bytes[i + 2] == 189)
                    {
                        return true;
                    }
            }
            return false;
        }
        /// <summary>
        /// 将Html文字 输出到PDF档里
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public byte[] ConvertHtmlTextToPDF(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //避免当htmlText无任何html tag标签的纯文字时，转PDF时会挂掉，所以一律加上<p>标签
            htmlText = "<p>" + htmlText + "</p>";

            MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流
            byte[] data = System.Text.Encoding.UTF8.GetBytes(htmlText);//字串转成byte[]
            MemoryStream msInput = new MemoryStream(data);
            Document doc = new Document();//要写PDF的文件，建构子没填的话预设直式A4
            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //指定文件预设开档时的缩放为100%

            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //开启Document文件 
            doc.Open();
            //使用XMLWorkerHelper把Html parse到PDF档里
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, System.Text.Encoding.UTF8, new UnicodeFontFactory());

            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, System.Text.Encoding.UTF8, new UnicodeFontFactory());

            //将pdfDest设定的资料写到PDF档
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();
            //回传PDF档案 
            return outputStream.ToArray();

        }

        //设置字体类
        public class UnicodeFontFactory : FontFactoryImp
        {
            private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialuni.ttf");//arial unicode MS是完整的unicode字型。
            private static readonly string 标楷体Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "KAIU.TTF");//标楷体

            public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
            {
                // 也可以使用 TTF 字体
                BaseFont bf0 = BaseFont.CreateFont("C:/WINDOWS/Fonts/SIMYOU.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                BaseFont bfChiness = BaseFont.CreateFont(@"C:\Windows\Fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                //可用Arial或标楷体，自己选一个
                BaseFont baseFont = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                return new Font(bfChiness, size, style, color);
            }
        }
        */
    }
}