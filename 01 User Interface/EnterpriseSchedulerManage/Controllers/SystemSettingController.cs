using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnterpriseSchedulerManage.Controllers
{
    public class SystemSettingController : Controller
    {
        // GET: SystemSetting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonalInfo()
        {
            return View();
        }


        #region 获取同步内容
        /// <summary>
        ///  获取同步内容
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSyschronContext()
        {
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            Dict.Add("HPHP", "医院码");
            Dict.Add("DADA", "诊断码");
            Dict.Add("SPSP", "诊疗码");
            Dict.Add("SPCT", "药品码");
            Dict.Add("DIDI", "药品适应症");
            Dict.Add("DNDN", "药品码别名");
            Dict.Add("SPON", "诊疗码别名");
            Dict.Add("DODO", "剂型码");
            Dict.Add("DOOT", "剂型别名");
            Dict.Add("SHLS", "诊疗目录");
            Dict.Add("STSP", "服务诊疗匹配");
            Dict.Add("DCDC", "药品分类");
            return Json(new { dic = Dict }, JsonRequestBehavior.AllowGet);
        }
        #endregion 


        public JsonResult GetTargetData()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}