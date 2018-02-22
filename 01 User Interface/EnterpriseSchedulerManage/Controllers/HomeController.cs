using EnterpriseSchedulerManage.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseSchedulerManage.Controllers
{
    public class HomeController : Controller
    {
        [UserInfoConfirm]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Clear();
            return Redirect("../Users/Index");
        }
    }
}