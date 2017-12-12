using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var list = new List<SPEH_INDEX_CAROUSEL_IMAGE_INFO_LIST_RESULT>();
            if (UserInfo != null)
                list = CommonBl.QuerySingle<SPEH_INDEX_CAROUSEL_IMAGE_INFO_LIST, SPEH_INDEX_CAROUSEL_IMAGE_INFO_LIST_RESULT>(new SPEH_INDEX_CAROUSEL_IMAGE_INFO_LIST { pUSUS_ID = UserInfo.USUS_ID });
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [UserInfoIsConfirm]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}