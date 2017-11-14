using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;
using System.Dynamic;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    [Authorization]
    public class InsurantController : BaseController
    {
        // GET: Insurant
        public ActionResult Index()
        {
            var lang = Request.Cookies["defaultLang"]?.Value == "en";
            //员工资讯
            var employeeInfoList = CommonBl.QuerySingle<SPEH_FMDT_FAMILY_DETL_LIST_WEB, SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT>(new SPEH_FMDT_FAMILY_DETL_LIST_WEB { pFMFM_KY = UserInfo.USUS_KY });

            //家庭成员
            var famliyList = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(new SPEH_MEME_MEMBER_INFO_LIST_WEB { pEHUSER = UserInfo.USUS_ID });
            if (lang) famliyList.ForEach(x =>
                        {
                            x.SYSV_MEME_REL_CD_DESC = x.SYSV_MEME_REL_CD_DESC_ENG;
                            x.SYSV_SEX_DESC = x.SYSV_SEX_DESC_ENG;
                        });

            //福利信息
            var benefitInfoList = CommonBl.QuerySingle<SPEH_PLFM_LIST, SPEH_PLFM_LIST_RESULT>(new SPEH_PLFM_LIST { pFMFM_KY = UserInfo.USUS_KY });
            if (lang) benefitInfoList.ForEach(x =>
                        {
                            x.SYSV_MEME_REL_CD = x.SYSV_MEME_REL_CD_ENG;
                        });


            //积分使用记录
            var pointsRecordsList = CommonBl.QuerySingle<SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB, SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB_RESULT>(new SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB { pFMFM_KY = UserInfo.USUS_KY });

            dynamic model = new ExpandoObject();
            model.MEMBER_ID = UserInfo.USUS_ID.Split('-').Length >= 2 ? UserInfo.USUS_ID.Split('-')[1] : "";
            model.GPGP_NAME = UserInfo.GPGP_NAME;
            model.NAME = UserInfo.NAME;
            model.GCGC_DESC = UserInfo.GCGC_DESC;

            model.EmployeeInfoList = employeeInfoList;
            model.FamliyList = famliyList;
            model.BenefitInfoList = benefitInfoList;
            model.PointsRecordsList = pointsRecordsList;
            return View(model);
        }

        public JsonResult FindUserIsConfirmMemberInfo()
        {
            var entity = new SPEH_USUS_IS_CONFIRM_SELECT { pUSUS_ID = UserInfo.USUS_ID };
            CommonBl.Execute(entity);
            return Json(entity.pUSUS_INFO_IS_CONFIRM, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditUserIsConfirmMemberInfo(string id)
        {
            var entity = new SPEH_USUS_IS_CONFIRM_UPDATE { pUSUS_ID = UserInfo.USUS_ID, pUSUS_INFO_IS_CONFIRM = id };
            CommonBl.Execute(entity);
            if (entity.ReturnValue == 1) UserInfo.USUS_INFO_IS_CONFIRM = entity.pUSUS_INFO_IS_CONFIRM;
            return Json(entity.ReturnValue == 1 ? "success" : "failed");
        }

        public ActionResult BillingInfomationDetail(string fmacKy)
        {
            var entity = new SPEH_FMAD_ACCOUNT_DET_LIST() { pFMAC_KY = fmacKy, pEHUSER = UserInfo.USUS_ID };
            var list = CommonBl.QuerySingle<SPEH_FMAD_ACCOUNT_DET_LIST, SPEH_FMAD_ACCOUNT_DET_LIST_RESULT>(entity);
            return View(list);
        }

        public ActionResult BenefitDetail(string plplKy, string memeKy)
        {
            var entity = new SPEH_PLFM_DET_LIST()
            {
                pPLPL_KY = plplKy,
                pMEME_KY = memeKy,
                pEHUSER = UserInfo.USUS_ID
            };
            var list = CommonBl.QuerySingle<SPEH_PLFM_DET_LIST, SPEH_PLFM_DET_LIST_RESULT>(entity);
            return View(list);
        }
    }
}