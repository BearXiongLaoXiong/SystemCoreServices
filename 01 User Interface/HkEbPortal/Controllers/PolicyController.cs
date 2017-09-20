using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Controllers
{
    public class PolicyController : Controller
    {
        private readonly ICommonBl _commonBl = new CommonBl();
        // GET: Policy
        public ActionResult Index(int memeKy = 0)
        {
            var selectList = new SelectList(new[] { "开放", "有效", "终止", "全部" });
            ViewData["MemeDropDownList"] = selectList;
            return View();
        }

        public JsonResult FindView(string status)
        {
            var entity = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB
            {
                pEHUSER = "fmfm",
                pSYSV_PLPL_STS = status
            };
            var list = _commonBl.QueryMultiple<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0,SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT1,SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT2> (entity);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}