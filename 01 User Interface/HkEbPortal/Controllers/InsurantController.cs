
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    public class InsurantController : Controller
    {
        private readonly ICommonBl _commonBl = new CommonBl();

        // GET: Insurant
        public ActionResult Index()
        {
            // 默认返回员工详细信息
            var entity = new SPEH_FMDT_FAMILY_DETL_LIST_WEB() { pFMFM_KY = "10001" };
            var list = _commonBl.QuerySingle<SPEH_FMDT_FAMILY_DETL_LIST_WEB,SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT>(entity);

            return View(list);
        }
    }
}