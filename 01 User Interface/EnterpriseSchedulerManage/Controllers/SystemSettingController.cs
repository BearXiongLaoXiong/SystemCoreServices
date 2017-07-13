using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SystemCore.BusinessLogic;
using SystemCore.Entities.SystemSetting;

namespace EnterpriseSchedulerManage.Controllers
{
    public class SystemSettingController : Controller
    {
        private readonly SynChronCode synChronCode = new SynChronCode();

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
            string option = "";
            List<SPEH_SYSV_VALUE_LIST_RESULT> list = synChronCode.GetSynChronContent();
            foreach (var item in list)
            {
                option += "<option value='" + item.value + "'>" + item.text + "</option>";
            }
            return Json(new { Option = option }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region  获取同步目标环境
        /// <summary>
        ///  获取同步目标环境
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTargetData()
        {
           return Json(new { TartData = synChronCode.GetSynChronTarget() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 根据参数获取对应的值
        /// <summary>
        /// 根据参数获取对应的值
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSynchronValues(string synchronContent,string source,string codeValue,string startDate,string endDate)
        {
            object resultStr = null;
            switch (synchronContent)
            {
                case "HPHP": // 医院码
                    resultStr = synChronCode.GetHPHPCodeByCondition(synchronContent, source, codeValue, startDate, endDate); 
                    break;
                case "DADA": // 诊断码
                    resultStr = synChronCode.GetDADACodeByCondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "SPSP": // 诊疗码
                    resultStr = synChronCode.GetSPSPCodeByCondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "SPCT": //药品码
                    resultStr = synChronCode.GetSPCTCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "DIDI": //药品适应症
                    resultStr = synChronCode.GetDIDICodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "DNDN": //药品码别名
                    resultStr = synChronCode.GetDNDNCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "SPON": //诊疗码别名
                    resultStr = synChronCode.GetSPONCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "DODO": //剂型码
                    resultStr = synChronCode.GetDODOCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "DOOT": //剂型别名
                    resultStr = synChronCode.GetDOOTCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "SHLS": //诊疗目录
                    resultStr = synChronCode.GetSHLSCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "STSP": //服务诊疗匹配
                    resultStr = synChronCode.GetSTSPCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                case "DCDC": //药品分类
                    resultStr = synChronCode.GetDCDCCodeBycondition(synchronContent, source, codeValue, startDate, endDate);
                    break;
                default:
                    break;
            }
            return Json(new { Obj = resultStr }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 同步码
        /// <summary>
        ///  同步码
        /// </summary>
        /// <param name="TargetArray"></param>
        /// <param name="CodeArray"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SynChronCodeByTarget(string SynChronContent,string[] TargetArray,string[] CodeArray,object data)
        {
            string resultStr = "";
            switch (SynChronContent)
            {
                case "HPHP": // 医院码
                    resultStr = "更新医院码成功";
                    break;
                case "DADA": // 诊断码
                    resultStr = "更新诊断码成功";
                    break;
                case "SPSP": // 诊疗码
                    resultStr = "更新诊疗码成功";
                    break;
                case "SPCT": //药品码
                    resultStr = "更新药品码成功";
                    break;
                case "DIDI": //药品适应症
                    resultStr = "更新药品适应症成功";
                    break;
                case "DNDN": //药品码别名
                    resultStr = "更新药品码别名成功";
                    break;
                case "SPON": //诊疗码别名
                    resultStr = "更新药品码别名成功";
                    break;
                case "DODO": //剂型码
                    resultStr = "更新药品码别名成功";
                    break;
                case "DOOT": //剂型别名
                    resultStr = "更新药品码别名成功";
                    break;
                case "SHLS": //诊疗目录
                    resultStr = "更新诊疗目录成功";
                    break;
                case "STSP": //服务诊疗匹配
                    resultStr = "更新服务诊疗匹配成功";
                    break;
                case "DCDC": //药品分类
                    resultStr = "更新药品分类成功";
                    break;
                default:
                    break;
            }
            return Json(resultStr, JsonRequestBehavior.AllowGet);
        }
        #endregion 
    }
}