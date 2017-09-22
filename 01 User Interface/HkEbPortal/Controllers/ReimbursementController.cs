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
            var dic = new Dictionary<string, string>();
            dic.Add("0", "本人-代小武");
            dic.Add("1", "兒子");
            dic.Add("2", "妻子");
            dic.Add("3", "女兒");
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
            //var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_INSERT()
            //{

            //};
            //_commonBl.Execute(entity);
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
            var selectEBlist = new SelectList(list, "EBEB_KY", "EBEB_DESC");
            var selectIVlist = new SelectList(ivlist,"value","text");
            ViewData["EBEB_DropDownList"] = selectEBlist;
            ViewData["CLIV_DropDownList"] = selectIVlist;

            var editentity = new SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT(){ pCLIV_KY = "8365270" };
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
        public JsonResult Delete()
        {
            var del = new SPEH_CLIV_CLAIM_INVOICE_INFO_DELETE() { };
            _commonBl.Execute(del);

            return Json("",JsonRequestBehavior.AllowGet);
        }
    }
}