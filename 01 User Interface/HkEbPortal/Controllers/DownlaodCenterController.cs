using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Controllers
{
    public class DownlaodCenterController : BaseController
    {
        [Authorization]
        [UserInfoIsConfirm]
        public ActionResult Index()
        {
            var defaultLang = Request.Cookies["defaultLang"];
            var policyDocList = CommonBl.QuerySingle<SPEH_PLPL_DOC_LIST, SPEH_PLPL_DOC_LIST_RESULT>(new SPEH_PLPL_DOC_LIST { pUSUS_KY = int.Parse(UserInfo.USUS_KY), pID_KY = int.Parse(UserInfo.GPGP_KY), lang = defaultLang?.Value });
            return View(policyDocList.Where(x => x.IS_ACTIVE == "0"));
        }
    }
}