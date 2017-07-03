using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseSchedulerManage.Controllers
{
    /// <summary>
    /// 任务调度
    /// </summary>
    public class TaskSchedulerController : Controller
    {
        // GET: TaskScheduler
        public ActionResult Index()
        {
            return View();
        }
    }
}