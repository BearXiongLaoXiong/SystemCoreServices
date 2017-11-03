
using BusinessLogicRepository;
using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    [Authorization]
    public class InsurantController : BaseController
    {
        // GET: Insurant
        public ActionResult Index()
        {
            // 员工资讯
            var employeeInfoList = CommonBl.QuerySingle<SPEH_FMDT_FAMILY_DETL_LIST_WEB, SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT>(new SPEH_FMDT_FAMILY_DETL_LIST_WEB { pFMFM_KY = UserInfo.USUS_KY });

            //家庭成员
            var famliyList = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(new SPEH_MEME_MEMBER_INFO_LIST_WEB { pEHUSER = UserInfo.USUS_ID });

            //福利信息
            var benefitInfoList = CommonBl.QuerySingle<SPEH_PLFM_LIST, SPEH_PLFM_LIST_RESULT>(new SPEH_PLFM_LIST { pFMFM_KY = UserInfo.USUS_KY });

            //积分使用记录
            var pointsRecordsList = CommonBl.QuerySingle<SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB, SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB_RESULT>(new SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB { pFMFM_KY = UserInfo.USUS_KY });

            dynamic model = new ExpandoObject();
            model.MEMBER_ID = UserInfo.USUS_ID.Split('-').Length >= 2 ? UserInfo.USUS_ID.Split('-')[1] : ""; ;
            model.GPGP_NAME = UserInfo.GPGP_NAME;
            model.NAME = UserInfo.NAME;
            model.GCGC_DESC = UserInfo.GCGC_DESC;

            model.EmployeeInfoList = employeeInfoList;
            model.FamliyList = famliyList;
            model.BenefitInfoList = benefitInfoList;
            model.PointsRecordsList = pointsRecordsList;
            return View(model);
        }

        ///// <summary>
        ///// 家庭成员
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult GetFamilyInfo()
        //{
        //    var entity = new SPEH_MEME_MEMBER_INFO_LIST_WEB() { pEHUSER = UserInfo.USUS_ID };
        //    var list = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(entity);

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 被保险人生活方式
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLifeStyle()
        {
            var entity = new SPEH_MELS_LIFESTYLE_LINK_LIST() { pMEME_KY = UserInfo.MEME_KY, pEHUSER = UserInfo.USUS_ID };
            var list = CommonBl.QuerySingle<SPEH_MELS_LIFESTYLE_LINK_LIST, SPEH_MELS_LIFESTYLE_LINK_LIST_RESULT>(entity);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 被保险人地区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMemeberArea()
        {
            var entity = new SPEH_MESH_SHIP_WORK_LINK_LIST() { };
            var list = CommonBl.QuerySingle<SPEH_MESH_SHIP_WORK_LINK_LIST, SPEH_MESH_SHIP_WORK_LINK_LIST_RESULT>(entity);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult GetPolicyInfo()
        //{
        //    var entity = new SPEH_PLFM_LIST() { pFMFM_KY = UserInfo.USUS_KY };
        //    var list = CommonBl.QuerySingle<SPEH_PLFM_LIST, SPEH_PLFM_LIST_RESULT>(entity);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        /// <summary>
        /// 账单信息
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult GetBillingInfomation()
        //{
        //    var entity = new SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB() { pFMFM_KY = UserInfo.USUS_KY };
        //    var list = CommonBl.QuerySingle<SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB, SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB_RESULT>(entity);

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult BillingInfomationDetail(string FMAC_KY)
        {
            var entity = new SPEH_FMAD_ACCOUNT_DET_LIST() { pFMAC_KY = FMAC_KY };
            var list = CommonBl.QuerySingle<SPEH_FMAD_ACCOUNT_DET_LIST, SPEH_FMAD_ACCOUNT_DET_LIST_RESULT>(entity);
            return View(list);
        }

        public ActionResult BenefitDetail(string PLPL_KY, string MEME_KY)
        {
            var entity = new SPEH_PLFM_DET_LIST()
            {
                pPLPL_KY = PLPL_KY,
                pMEME_KY = MEME_KY
            };
            var list = CommonBl.QuerySingle<SPEH_PLFM_DET_LIST, SPEH_PLFM_DET_LIST_RESULT>(entity);
            return View(list);
        }
    }
}