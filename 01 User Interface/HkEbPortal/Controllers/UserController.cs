using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Controllers
{
    public class UserController : Controller
    {
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
                return Json(new { Code = 1, Msg = "您输入的账号不存在或者密码错误!", }, JsonRequestBehavior.DenyGet);

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


        [HttpPost]
        public JsonResult SignUp(string txtPolicyUp, string txtMemberUp, string txtEmailUp = "")
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

            //在HK_EB_DATE中找到了对应的数据 且ususID已注册 且email已注册过
            if (userInfo.USUS_ID.Length > 0 && userInfo.USUS_EMAIL.Length > 0)
                return Json(new { Code = 2, Msg = "您的账号已存在!" }, JsonRequestBehavior.DenyGet);

            if (txtEmailUp.Length == 0)
                return Json(new { Code = 3, Msg = "请输入你的邮件!" }, JsonRequestBehavior.DenyGet);

            //在HK_EB_DATE中找到了对应的数据 但userId未注册
            if (userInfo.USUS_ID.Length == 0)
            {
                var insert = new SPEH_USUS_EMAIL_INSERT
                {
                    pPolicy_NO = txtPolicyUp,
                    pCert_No = txtMemberUp,
                    pEmail = txtEmailUp
                };
                _commonBl.Execute(insert);
                EmailHelper.SendSmtpMail("bear.xiong@ensurlink.com.cn",
                    "bear.xiong@ensurlink.com.cn",
                    "!QAZ2wsx",
                    new[] { "164470250@qq.com" },
                    new string[] { },
                    "主题:HK_Portal 注册",
                    "征文:你的密码是11122333",
                    new string[] { },
                    out string result);
                return Json(new { Code = insert.ReturnValue, Msg = insert.ReturnValue == 1 ? "注册账号成功!" : "注册账号失败!" }, JsonRequestBehavior.DenyGet);
            }


            if (userInfo.USUS_ID.Length > 0 && userInfo.USUS_EMAIL.Length == 0)
            {
                var update = new SPEH_USUS_EMAIL_UPDATE
                {
                    pPolicy_NO = txtPolicyUp,
                    pCert_No = txtMemberUp,
                    pEmail = txtEmailUp
                };
                _commonBl.Execute(update);
                //todo 发送邮件
                return Json(new { Code = update.ReturnValue, Msg = update.ReturnValue == 1 ? "注册邮箱成功!" : "注册邮箱失败!" }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { Code = "999", Msg = "出现错误!" });
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ForgotPassword(string policyNo, string memberId)
        {
            string policy = Request["policyNo"];
            string memeber = Request["memberId"];
            if (string.IsNullOrEmpty(policy) || string.IsNullOrEmpty(memeber)) return Json(new { Data = 9, Msg = "请输入保单号/被保人" }, JsonRequestBehavior.AllowGet);
            var entity = new SPEH_USUS_USER_PWD_INFO_SELECT() { pPLPL_NO = policyNo, pMEME_ID = memberId };
            var list = _commonBl.QuerySingle<SPEH_USUS_USER_PWD_INFO_SELECT, SPEH_USUS_USER_PWD_INFO_SELECT_RESULT>(entity);

            return Json(new { Data = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.AllowGet);
        }
    }
}