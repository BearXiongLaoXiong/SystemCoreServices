using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        public ActionResult Index(int memeKy = 0)
        {
            var selectList = new SelectList(new[] { "开放", "有效", "终止", "全部" });
            ViewData["MemeDropDownList"] = selectList;
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}