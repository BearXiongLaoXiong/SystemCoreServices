
using BusinessLogicRepository;
using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;
using System;
using System.Collections.Generic;
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
            // 默认返回员工详细信息
            var entity = new SPEH_FMDT_FAMILY_DETL_LIST_WEB() { pFMFM_KY = UserInfo.FMFM_KY };
            var list = CommonBl.QuerySingle<SPEH_FMDT_FAMILY_DETL_LIST_WEB,SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT>(entity);
            ViewData["GPGPName"] = list?.First()?.GCGC_DESC;
            return View(list);
        }

        /// <summary>
        /// 家庭成员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFamilyInfo()
        {
            var entity = new SPEH_MEME_MEMBER_INFO_LIST_WEB() { pEHUSER = UserInfo.USUS_ID };
            var list = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(entity);
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

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

        [HttpPost]
        public JsonResult GetPolicyInfo ()
        {
            var entity = new SPEH_PLFM_LIST() { pFMFM_KY = UserInfo.FMFM_KY };
            var list = CommonBl.QuerySingle<SPEH_PLFM_LIST, SPEH_PLFM_LIST_RESULT>(entity);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 账单信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBillingInfomation()
        {
            var entity = new SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB() { pEHUSER = UserInfo.USUS_ID };
            var list = CommonBl.QuerySingle<SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB, SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB_RESULT>(entity);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}