using System;
using System.Collections.Generic;
using System.Dynamic;
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

        [HttpGet]
        public JsonResult FindView(string status, string name)
        {
            var entity = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB
            {
                pEHUSER = "fmfm",
                pSYSV_PLPL_STS = status
            };
            var result = _commonBl.QueryMultiple<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT1, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT2>(entity);
            return Json(new { names = result.ListSecond.GroupBy(g => g.MEME_NAME).Select(x => new { Name = x.Key, Count = x.Count() }), table = result.ListSecond.Where(x => x.MEME_NAME == name) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(string plplKy, string memeKy)
        {
            var entity = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB
            {
                pEHUSER = "fmfm",
                pPLPL_KY = plplKy,
                pMEME_KY = memeKy
            };
            var result = _commonBl.QueryMultiple<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT1, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT2, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT4>(entity);

            dynamic model = new ExpandoObject();
            model.Name = result.ListSecond.FirstOrDefault(x => x.PLPL_KY == plplKy)?.MEME_NAME;
            model.Desc = result.ListSecond.FirstOrDefault(x => x.PLPL_KY == plplKy)?.PLPL_DESC;
            model.PlplList = result.ListThird;
            model.PlplInfoList = result.ListFour.Take(5);
            return View(model);
        }


    }
}