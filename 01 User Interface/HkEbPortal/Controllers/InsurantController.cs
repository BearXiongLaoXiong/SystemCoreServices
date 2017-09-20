using HKEBPortal.BusinessLogic;
using HKEBPortal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    public class InsurantController : Controller
    {
        private InsurantBusiness insurant = new InsurantBusiness();
        // GET: Insurant
        public ActionResult Index()
        {
            // 默认返回员工详细信息
            List<SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT> list = insurant.GetMemberInfoList();

            return View(list);
        }
    }
}