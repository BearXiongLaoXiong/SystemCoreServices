using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    public class InsurantController : Controller
    {
        // GET: Insurant
        public ActionResult Index()
        {
            // 默认返回员工详细信息

            return View();
        }
    }
}