﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;
using Newtonsoft.Json;

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
        public JsonResult FindView(string status, string memeKy)
        {
            var entity = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB
            {
                pEHUSER = "fmfm",
                pSYSV_PLPL_STS = status
            };
            var result = _commonBl.QueryMultiple<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT1, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT2>(entity);
            return Json(new { names = result.ListSecond.Select(x => new { MemeKy = x.MEME_KY, Name = x.MEME_NAME }).Distinct(), table = result.ListSecond.Where(x => memeKy.Length > 0 ? x.MEME_KY == memeKy : x.MEME_KY == result.ListSecond.FirstOrDefault()?.MEME_KY) }, JsonRequestBehavior.AllowGet);
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
            var pdctIdList = result.ListThird.Select(x => x.PDCT_ID);

            dynamic model = new ExpandoObject();
            model.Name = result.ListSecond.FirstOrDefault(x => x.PLPL_KY == plplKy)?.MEME_NAME;
            model.Desc = result.ListSecond.FirstOrDefault(x => x.PLPL_KY == plplKy)?.PLPL_DESC;
            model.PlplList = result.ListThird;
            model.PlplInfoList = result.ListFour.Where(x => pdctIdList.Contains(x.PDCT_ID));
            return View(model);
        }

        [HttpPost]
        public string Detail(string data)
        {
            string result = "";
            var json = JsonConvert.DeserializeObject<List<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT4>>(data);

            foreach (var item in json)
            {
                var insert = new SPEH_AQHT_HTML_INFO_INSERT
                {
                    pMEME_KY = int.Parse(item.MEME_KY),
                    pPLPL_KY = int.Parse(item.PLPL_KY),
                    pPDPD_ID = item.PDPD_ID,
                    pEHUSER = "fmfm"
                };
                _commonBl.Execute(insert);
                if (insert.ReturnValue == 0)
                {
                    var update = new SPEH_PLME_MEM_UPDATE
                    {
                        pMEME_KY = int.Parse(item.MEME_KY),
                        pPLPL_KY = int.Parse(item.PLPL_KY),
                        pPDCT_ID = item.PDCT_ID,
                        pPDPD_ID = item.PDPD_ID,
                        pEHUSER = "fmfm"
                    };
                    _commonBl.Execute(update);
                }
                result = insert.pRTN_MSG;
            }

            return result;
        }


        public ActionResult Information(string pdpdId = "PD010135")
        {
            var entity = new SPEH_PDPD_PRODUCTBASIC_SELECT { pPDPD_ID = pdpdId };
            var result = _commonBl.QueryMultiple<SPEH_PDPD_PRODUCTBASIC_SELECT, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT1, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT2, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT3, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT4>(entity);
            dynamic model = new ExpandoObject();
            model.BasicInfo = result.ListFirst.FirstOrDefault();
            model.PdfInfo = result.ListSecond;

            var dict = new Dictionary<string, int>();
            foreach (var item in result.ListFour)
            {
                if (dict.Keys.Contains(item.TITLE)) item.TITLE = "";
                else dict.Add(item.TITLE, 1);
            }
            model.TreeInfo = result.ListFour;
            model.DescInfo = result.ListThird;
            return View(model);
        }


    }
}