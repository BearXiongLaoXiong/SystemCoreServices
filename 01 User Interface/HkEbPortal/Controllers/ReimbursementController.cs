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
            var fmfmentity = new SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB() { };
            var fmfmlist = _commonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB, SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB_RESULT>(fmfmentity);
            var entity = new SPEH_EBEB_VALUE_LIST() { };
            var list = _commonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(entity);
            var ivtype = new SPEH_SYSV_VALUE_LIST() { pSYSV_TYPE = "SYSV_CLIV_TYPE" };
            var ivlist = _commonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(ivtype);
            var selectFMlist = new SelectList(fmfmlist, "CLIV_KY", "FMFM_NAME");
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
            string cliv_ky = form["CLIV_DropDownList"];
            string cliv_id1 = form["CLIV_ID"];
            string date = form["date"];
            string date1 = form["date1"];
            string apply_amt = form["APPLY_AMT"];
            string cliv_chg = form["CLIV_CHG"]; 
            string comment = form["COMMENT"];
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_INSERT()
            {
                pMEME_KY = meme_ky,
                pEBEB_KY = ebeb_ky,
                pCLIV_KY =cliv_ky,
                pCLIV_ID = cliv_id1,
                pCLIV_APP_DT = date,
                pCLIV_STS_DTM = date1,
                pCLIV_APPLY_AMT = apply_amt,
                pCLIV_CHG = cliv_chg,
                pCLIV_COMMENT = comment
            };
            _commonBl.Execute(entity);
            if (string.IsNullOrEmpty(comment))
            {
                return null;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string clivKy)
        {
            var fmfmentity = new SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB() {  };
            var fmfmlist = _commonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB, SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB_RESULT>(fmfmentity);
            var entity = new SPEH_EBEB_VALUE_LIST() { };
            var list = _commonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(entity);
            var ivtype = new SPEH_SYSV_VALUE_LIST() { pSYSV_TYPE = "SYSV_CLIV_TYPE" };
            var ivlist = _commonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(ivtype);
            var selectFMlist = new SelectList(fmfmlist, "CLIV_KY", "FMFM_NAME");
            var selectEBlist = new SelectList(list, "EBEB_KY", "EBEB_DESC");
            var selectIVlist = new SelectList(ivlist,"value","text");
            ViewData["FMFM_DropDownList"] = selectFMlist;
            ViewData["EBEB_DropDownList"] = selectEBlist;
            ViewData["CLIV_DropDownList"] = selectIVlist;

            var editentity = new SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT(){ pCLIV_KY = clivKy };
            var result = _commonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT, SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT_RESULT>(editentity);
            return View(result?.First());
        }

        [HttpPost]
        public JsonResult EditUpdate()
        {
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_UPDATE() { };
            _commonBl.Execute(entity);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string CLIV_KY)
        {
            var del = new SPEH_CLIV_CLAIM_INVOICE_INFO_DELETE() { pCLIV_KY = CLIV_KY };
            _commonBl.Execute(del);

            return Json(del.pRTN_MSG, JsonRequestBehavior.AllowGet);
        }
    }
}