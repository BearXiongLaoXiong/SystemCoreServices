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

        [HttpPost]
        public JsonResult Add()
        {
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_INSERT()
            {
                
            };
            _commonBl.Execute(entity);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit()
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