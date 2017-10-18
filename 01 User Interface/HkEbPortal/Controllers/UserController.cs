using System;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;
using System.IO;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;

namespace HkEbPortal.Controllers
{
    public class UserController : Controller
    {

        private readonly string _from = ConfigurationManager.AppSettings["EmailFrom"];
        private readonly string _userName = ConfigurationManager.AppSettings["EmailUsername"];
        private readonly string _host = ConfigurationManager.AppSettings["EmailHost"];
        private readonly string _passWord = ConfigurationManager.AppSettings["EmailPassword"];

        private readonly ICommonBl _commonBl = new CommonBl();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string txtpolicyNo, string txtMember, string txtPassword)
        {
            var entity = new SPEH_FMFM_LOGIN
            {
                pPOLICYNO = txtpolicyNo,
                pUSUS_ID = txtMember,
                pUSUS_PSWD = txtPassword
            };
            var userInfo = _commonBl.QuerySingle<SPEH_FMFM_LOGIN, UserInfo>(entity).FirstOrDefault();
            if (userInfo == null)
                return Json(new { Code = 1, Msg = "您输入的账号不存在或者密码错误!" }, JsonRequestBehavior.DenyGet);

            if (userInfo.USUS_EMAIL.Length == 0)
                return Json(new { Code = 2, Msg = "没有发现您的邮箱,请到注册界面激活邮箱!" }, JsonRequestBehavior.DenyGet);

            if (userInfo.USUS_EMAIL_ISACTIVE == "0")
                return Json(new { Code = 3, Msg = userInfo.USUS_EMAIL }, JsonRequestBehavior.DenyGet);

            Session.RemoveAll();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                userInfo.USUS_KY,
                DateTime.Now,
                DateTime.Now.AddHours(12),
                false,//將管理者登入的 Cookie 設定成 Session Cookie
                userInfo.NAME + "\t" + userInfo.GPGP_NAME,//userdata看你想存放啥
                FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            Session[FormsAuthentication.FormsCookieName] = userInfo;

            return Json(new { Code = 0, Msg = "", Data = new { userInfo.NAME, userInfo.GPGP_NAME } }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmEmail(string txtpolicyNo, string txtMember, string txtPassword)
        {
            var entity = new SPEH_USUS_EMAIL_ISACTIVE_UPDATE
            {
                pPolicy_NO = txtpolicyNo,
                pCert_No = txtMember,
                pPassWord = txtPassword
            };
            _commonBl.Execute(entity);

            return Json(new { Code = entity.ReturnValue, Msg = entity.ReturnValue == 1 ? "邮箱激活成功!" : "邮箱激活失败!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SignUp(string txtPolicyUp, string txtMemberUp,string txtBirthday, string txtEmailUp = "")
        {
            var entity = new SPEH_USUS_EMAIL_SELECT_ISACTIVE
            {
                pPolicy_NO = txtPolicyUp,
                pCert_No = txtMemberUp
            };
            var userInfo = _commonBl.QuerySingle<SPEH_USUS_EMAIL_SELECT_ISACTIVE, SPEH_USUS_EMAIL_ISACTIVE_RESULT>(entity).FirstOrDefault();
            //没有在HK_EB_DATE中找到对应的数据
            if (userInfo == null)
                return Json(new { Code = 1, Msg = "您输入的账号不存在!" }, JsonRequestBehavior.DenyGet);

            if (string.IsNullOrWhiteSpace(txtBirthday))
                return Json(new { Code = 2,Msg = "请输入日期!"}, JsonRequestBehavior.DenyGet);

            //在HK_EB_DATE中找到了对应的数据 且ususID已注册 且email已注册过
            if (userInfo.USUS_ID.Length > 0 && userInfo.USUS_SIGNUP_ISACTIVE.Equals("1"))
                return Json(new { Code = 2, Msg = "您的账号已注册!" }, JsonRequestBehavior.DenyGet);

            if (string.IsNullOrWhiteSpace(userInfo.DOB) || !userInfo.DOB.Equals(txtBirthday.Trim()))
                return Json(new { Code = 1, Msg = "无此被保险人，请联系团体HR!" }, JsonRequestBehavior.DenyGet);

            txtEmailUp = string.IsNullOrEmpty(txtEmailUp) ? userInfo.USUS_EMAIL : txtEmailUp;

            if (string.IsNullOrWhiteSpace(userInfo.USUS_EMAIL) && string.IsNullOrWhiteSpace(txtEmailUp))
                return Json(new { Code = 3, Msg = "请输入你的邮件" }, JsonRequestBehavior.DenyGet);
            else if (!string.IsNullOrWhiteSpace(userInfo.USUS_EMAIL) && userInfo.USUS_EMAIL_ISACTIVE =="0")
                return Json(new { Code = 4, Msg = userInfo.USUS_EMAIL }, JsonRequestBehavior.DenyGet);

            //在HK_EB_DATE中找到了对应的数据 但userId未注册
            if (userInfo.USUS_ID.Length > 0)
            {
                var insert = new SPEH_USUS_EMAIL_INSERT
                {
                    pPolicy_NO = txtPolicyUp,
                    pCert_No = txtMemberUp,
                    pEmail = txtEmailUp
                };
                _commonBl.Execute(insert);

                EmailHelper.SendSmtpMail(_host, _from, _userName, _passWord, new[] { txtEmailUp }, new string[] { },
                                        "主题:HK_Portal 注册",
                                        "征文:您的密码是123456",
                                        new string[] { }, out string result);

                return Json(new { Code = insert.ReturnValue, Msg = insert.ReturnValue == 1 ? "注册账号成功!" : "注册账号失败!" }, JsonRequestBehavior.DenyGet);
            }


            
            return Json(new { Code = "999", Msg = "出现错误!" });
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Clear();
            return Redirect("../eflexi/Home/Index");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ForgotPassword(string policyNo, string memberId)
        {
            var entity = new SPEH_USUS_USER_PWD_INFO_SELECT() { pPLPL_NO = policyNo, pMEME_ID = memberId };
            var list = _commonBl.QuerySingle<SPEH_USUS_USER_PWD_INFO_SELECT, SPEH_USUS_USER_PWD_INFO_SELECT_RESULT>(entity);

            return Json(new { Data = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.AllowGet);
        }

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

    }
}