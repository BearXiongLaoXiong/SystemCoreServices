using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BusinessLogicRepository;
using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;
using Newtonsoft.Json;
using System.IO;
using HkEbPortal.App_Start;

namespace HkEbPortal.Controllers
{
    [Authorization]
    public class PolicyController : BaseController
    {
        private static string savePath = ConfigurationManager.AppSettings["PdfSavePath"] ?? "";
        // GET: Policy
        public ActionResult Index()
        {
            if (new Common().IsOpenEnrollment(UserInfo.USUS_KY))
            {
                return Redirect("../eflexi/Home/Index");
            }
            var entity = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB
            {
                pEHUSER = UserInfo.USUS_ID
            };
            var result = CommonBl.QueryMultiple<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT1, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT2>(entity);

            dynamic model = new ExpandoObject();
            model.INITIAL_AMT = result.ListFirst.Sum(x => float.Parse(x.INITIAL_AMT));
            model.BlanAmount = result.ListFirst.Sum(x => float.Parse(x.BlanAmount));
            model.Names = result.ListFirst;
            model.Tables = result.ListThird;
            return View(model);
        }

        [HttpGet]
        public ActionResult FindView(string status, string memeKy)
        {
            var entity = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB
            {
                pEHUSER = UserInfo.USUS_ID,
                pSYSV_PLPL_STS = status
            };
            var result = CommonBl.QueryMultiple<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT0, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT1, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT2>(entity);
            return View(new { names = result.ListSecond, tables = result.ListThird });
            //return Json(new { names = result.ListSecond.Select(x => new { MemeKy = x.MEME_KY, Name = x.MEME_NAME }).Distinct(), table = result.ListThird.Where(x => memeKy.Length > 0 ? x.MEME_KY == memeKy : x.MEME_KY == result.ListSecond.FirstOrDefault()?.MEME_KY) }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Detail(string plplKy, string memeKy, string pdctId)
        {
            var entity = new SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB2
            {
                pEHUSER = UserInfo.USUS_ID,
                pPLPL_KY = plplKy,
                pMEME_KY = memeKy,
                pPDCT_ID = pdctId
            };
            var result = CommonBl.QuerySingle<SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB2, SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT4>(entity);
            var pdctIdList = result.Select(x => x.PDCT_ID);

            dynamic model = new ExpandoObject();
            model.PdctName = result.FirstOrDefault(x => x.PLPL_KY == plplKy)?.PDCT_NAME;
            model.Name = result.FirstOrDefault(x => x.PLPL_KY == plplKy)?.MEME_NAME;
            model.Desc = result.FirstOrDefault(x => x.PLPL_KY == plplKy)?.PLPL_ID;
            //model.PlplList = result.ListThird;
            model.PlplInfoList = result.Where(x => pdctIdList.Contains(x.PDCT_ID));
            model.PLGP_PATH = result.FirstOrDefault(x => x.PLPL_KY == plplKy)?.PLGP_PATH;
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
                    pEHUSER = UserInfo.USUS_ID
                };
                CommonBl.Execute(insert);
                if (insert.ReturnValue == 0)
                {
                    var update = new SPEH_PLME_MEM_UPDATE
                    {
                        pMEME_KY = int.Parse(item.MEME_KY),
                        pPLPL_KY = int.Parse(item.PLPL_KY),
                        pPDCT_ID = item.PDCT_ID,
                        pPDPD_ID = item.PDPD_ID,
                        pEHUSER = UserInfo.USUS_ID
                    };
                    CommonBl.Execute(update);
                }
                result = insert.pRTN_MSG;
            }
            return result;
        }


        public ActionResult Information(string pdpdId)
        {
            var entity = new SPEH_PDPD_PRODUCTBASIC_SELECT { pPDPD_ID = pdpdId };
            var result = CommonBl.QueryMultiple<SPEH_PDPD_PRODUCTBASIC_SELECT, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT1, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT2, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT3, SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT4>(entity);
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

        public FileStreamResult ReadPDF(string fName = "")
        {
            var fileName = $@"{savePath}\{fName}";
            if (fName.Length > 0 && System.IO.File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return File(fs, "application/pdf");
            }
            return null;
        }
    }
}