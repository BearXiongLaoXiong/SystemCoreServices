using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;
using Newtonsoft.Json;
using System.IO;

namespace HkEbPortal.Controllers
{
    [Authorization]
    //[UserInfoIsConfirm]
    [IsOpenEnrollment(true)]
    public class PolicyController : BaseController
    {
        private static readonly string SavePath = ConfigurationManager.AppSettings["PdfSavePath"] ?? "";
        // GET: Policy
        public ActionResult Index()
        {
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
        [ValidateAntiForgeryToken]
        public JsonResult Detail(string data)
        {
            string code = "";
            string msg = "";
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
                code = insert.ReturnValue.ToString();
                msg = insert.pRTN_MSG;
            }
            return Json(new { Code = code ,Msg = msg });
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

        public FileStreamResult ReadPdf(string fName = "")
        {
            var fileName = $@"{SavePath}\{fName}";
            if (fName.Length > 0 && System.IO.File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return File(fs, "application/pdf");
            }
            return null;
        }
    }
}