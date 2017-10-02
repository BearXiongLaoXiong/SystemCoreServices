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
        public JsonResult Login(string txtMember, string txtPassword)
        {
            var entity = new SPEH_FMFM_LOGIN
            {
                pUSUS_ID = txtMember,
                pUSUS_PSWD = txtPassword
            };
            var userInfo =_commonBl.QuerySingle<SPEH_FMFM_LOGIN, UserInfo>(entity).FirstOrDefault();
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

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ForgotPassword(string policyNo,string memberId)
        {
            string policy = Request["policyNo"];
            string memeber = Request["memberId"];
            if (string.IsNullOrEmpty(policy) || string.IsNullOrEmpty(memeber)) return Json(new { Data = 9, Msg = "请输入保单号/被保人" }, JsonRequestBehavior.AllowGet);
            var entity = new SPEH_USUS_USER_PWD_INFO_SELECT() { pPLPL_NO = policyNo ,pMEME_ID = memberId };
            var list = _commonBl.QuerySingle<SPEH_USUS_USER_PWD_INFO_SELECT, SPEH_USUS_USER_PWD_INFO_SELECT_RESULT>(entity);

            return Json(new { Data = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.AllowGet);
        }
    }
}