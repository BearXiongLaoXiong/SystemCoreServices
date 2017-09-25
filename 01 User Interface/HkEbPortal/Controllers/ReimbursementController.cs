using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    public class ReimbursementController : Controller
    {
        private readonly ICommonBl _commonBl = new CommonBl();

        // GET: Reimbursement
        public ActionResult Index()
        {
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB()
            {

            };
            var list = _commonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB, SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB_RESULT>(entity);
            return View(list);
        }

        public ActionResult Add()
        {
            var fmfmentity = new SPEH_MEME_MEMBER_INFO_LIST_WEB() { }; // 家庭成员
            var fmfmlist = _commonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(fmfmentity);
            var entity = new SPEH_EBEB_VALUE_LIST() { };
            var list = _commonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(entity);
            var ivtype = new SPEH_SYSV_VALUE_LIST() { pSYSV_TYPE = "SYSV_CLIV_TYPE" };
            var ivlist = _commonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(ivtype);
            var selectFMlist = new SelectList(fmfmlist, "MEME_KY", "MEME_NAME");
            var selectEBlist = new SelectList(list, "EBEB_KY", "EBEB_DESC");
            var selectIVlist = new SelectList(ivlist, "value", "text");
            ViewData["FMFM_DropDownList"] = selectFMlist;
            ViewData["EBEB_DropDownList"] = selectEBlist;
            ViewData["CLIV_DropDownList"] = selectIVlist;
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            string meme_ky = form["FMFM_DropDownList"];
            string ebeb_ky = form["EBEB_DropDownList"];
            string clivType = form["CLIV_DropDownList"];
            string clivID = form["CLIV_ID"];
            string clivDate = form["CLIV_Date"];
            string applyDate = form["Apply_Date"];
            string apply_amt = form["APPLY_AMT"];
            string cliv_chg = form["CLIV_CHG"]; 
            string comment = form["COMMENT"];
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_INSERT()
            {
                pMEME_KY = meme_ky,
                pEBEB_KY = ebeb_ky,
                pSYSV_CLIV_TYPE = clivType,
                pCLIV_ID = clivID,
                pCLIV_DT= clivDate,
                pCLIV_APP_DT = applyDate,
                pCLIV_APPLY_AMT = apply_amt,
                pCLIV_CHG = cliv_chg,
                pCLIV_COMMENT = comment
            };
            _commonBl.Execute(entity);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string clivKy)
        {
            if (string.IsNullOrEmpty(clivKy)) return View();
            var fmfmentity = new SPEH_MEME_MEMBER_INFO_LIST_WEB() { }; // 家庭成员
            var fmfmlist = _commonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(fmfmentity);
            var entity = new SPEH_EBEB_VALUE_LIST() { };
            var list = _commonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(entity);
            var ivtype = new SPEH_SYSV_VALUE_LIST() { pSYSV_TYPE = "SYSV_CLIV_TYPE" };
            var ivlist = _commonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(ivtype);
            var editentity = new SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT() { pCLIV_KY = clivKy };
            var result = _commonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT, SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT_RESULT>(editentity);
            var selectFMlist = new SelectList(fmfmlist, "MEME_KY", "MEME_NAME", result?.First()?.MEME_KY);
            var selectEBlist = new SelectList(list, "EBEB_KY", "EBEB_DESC", result?.First()?.EBEB_KY);
            var selectIVlist = new SelectList(ivlist, "value", "text", result?.First()?.CLIV_KY);
            ViewData["FMFM_DropDownList"] = selectFMlist;
            ViewData["EBEB_DropDownList"] = selectEBlist;
            ViewData["CLIV_DropDownList"] = selectIVlist;

            return View(result?.First());
        }

        [HttpPost]
        public ActionResult EditUpdate(FormCollection form)
        {
            if(string.IsNullOrEmpty(form["CLIV_KY"]))return Json("失敗！", JsonRequestBehavior.AllowGet);
            string cliv_ky = form["CLIV_KY"];
            string meme = form["FMFM_DropDownList"];
            string ebeb = form["EBEB_DropDownList"];
            string ivtype = form["CLIV_DropDownList"];
            string ivid = form["CLIV_ID"];
            string ivdate = form["CLIV_Date"];
            string appAMT = form["ApplyAMT"];
            string ivchg = form["CLIV_CHG"];
            string appdate = form["Apply_Date"];
            string comment = form["COMMENT"];
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_UPDATE()
            {
                pCLIV_KY= cliv_ky,
                pMEME_KY = meme,
                pEBEB_KY = ebeb,
                pSYSV_CLIV_TYPE = ivtype,
                pCLIV_ID = ivid,
                pCLIV_CHG = ivchg,
                pCLIV_APPLY_AMT= appAMT,
                pCLIV_DT = ivdate,
                pCLIV_APP_DT = appdate,
                pCLIV_COMMENT= comment
            };
            _commonBl.Execute(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(string CLIV_KY)
        {
            var del = new SPEH_CLIV_CLAIM_INVOICE_INFO_DELETE() { pCLIV_KY = CLIV_KY };
            _commonBl.Execute(del);

            return Json(del.pRTN_MSG, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Upload(string CLIV_KY)
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadImg()
        {
            string Str = "{\"result\":-1,\"message\":\"提交成功\",\"filename\":\"12424.jpg\",\"fileext\":\"撒的撒\"}";
            return Json(Str, JsonRequestBehavior.AllowGet);
        }
    }
}