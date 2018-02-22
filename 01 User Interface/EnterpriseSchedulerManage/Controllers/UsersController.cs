using EnterpriseSchedulerManage.Filters;
using EnterpriseSchedulerManage.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EnterpriseSchedulerManage.Controllers
{
    public class UsersController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string TxtNameId,string TxtPwdId)
        {
            Session.RemoveAll();

            SPEH_USUS_USER_INFO_LOGIN entity = new SPEH_USUS_USER_INFO_LOGIN() { pUSUS_NAME = TxtNameId,pUSUS_PSWD = TxtPwdId};
            var userInfo = CommonBl.QuerySingle<SPEH_USUS_USER_INFO_LOGIN, UserInfo>(entity)?.FirstOrDefault();
            if (userInfo != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                   userInfo.USUS_ID,
                   DateTime.Now,
                   DateTime.Now.AddHours(12),
                   false,//將管理者登入的 Cookie 設定成 Session Cookie
                   userInfo.USUS_NAME,//userdata看你想存放啥
                   FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Path = "/", Expires = DateTime.Now.AddHours(1), HttpOnly = true, Secure = true });
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                Session[FormsAuthentication.FormsCookieName] = userInfo;

                return Json(entity.pUSUS_MSG, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(entity.pUSUS_MSG, JsonRequestBehavior.DenyGet);
            }
        }
    }
}