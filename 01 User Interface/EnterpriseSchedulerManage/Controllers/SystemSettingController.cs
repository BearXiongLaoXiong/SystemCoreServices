using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            string option = "<option value='HPHP'>医院码</option>";
            option+= "<option value='DADA'>诊断码</option>";
            option += "<option value='SPSP'>诊疗码</option>";
            option += "<option value='SPCT'>药品码</option>";
            option += "<option value='DIDI'>药品适应症</option>";
            option += "<option value='DNDN'>药品码别名</option>";
            option += "<option value='SPON'>诊疗码别名</option>";
            option += "<option value='DODO'>剂型码</option>";
            option += "<option value='DOOT'>剂型别名</option>";
            option += "<option value='SHLS'>诊疗目录</option>";
            option += "<option value='STSP'>服务诊疗匹配</option>";
            option += "<option value='DCDC'>药品分类</option>";
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
            string targetOption = "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>初审-复星(永安)生产</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>核心-复星(永安)生产</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>初审-复星(永安)测试</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>核心-复星(永安)测试</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>初审-西安生产</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>核心-西安生产</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>GCL初审开发环境</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>GCL核心开发环境</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>GCL试用环境</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>GCL试用环境</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>中文新初审DEMO(兼太平开发)</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>中文核心DEMO(兼太平开发)</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>太平新初审UAT</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>太平核心UAT</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>太平新初审生产</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>太平核心生产</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>皓为新初审测试</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>皓为核心测试</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>皓为新初审生产</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>皓为核心生产</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>英文新初审DEMO</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>英文核心DEMO</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>云南初审开发</td><td>WorkFlow</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>云南核心开发</td><td>eHealth</td></tr>";
            targetOption += "<tr><td><input type='checkbox' name='' lay-skin='primary'></td><td>云南丘博核心开发</td><td>eHealth</td></tr>";


            return Json(new { TartData = targetOption }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 根据参数获取对应的值
        /// <summary>
        /// 根据参数获取对应的值
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSynchronValues(string synchronContent,string source,string codeValue,string startDate,string endDate)
        {
            string resultStr = "";
            switch (synchronContent)
            {
                case "HPHP": // 医院码
                    resultStr = GetHPHP_List(codeValue, startDate, endDate);
                    break;
                case "DADA": // 诊断码
                    resultStr = GetDADA_List(codeValue, startDate, endDate);
                    break;
                case "SPSP": // 诊疗码
                    resultStr = GetSPSP_List(codeValue, startDate, endDate);
                    break;
                case "SPCT": //药品码
                    resultStr = GetSPCT_List(codeValue, startDate, endDate);
                    break;
                case "DIDI": //药品适应症
                    resultStr = GetDIDI_List(codeValue, startDate, endDate);
                    break;
                case "DNDN": //药品码别名
                    resultStr = GetDNDN_List(codeValue, startDate, endDate);
                    break;
                case "SPON": //诊疗码别名
                    resultStr = GetDNDN_List(codeValue, startDate, endDate);
                    break;
                case "DODO": //剂型码
                    resultStr = GetDODO_List(codeValue, startDate, endDate);
                    break;
                case "DOOT": //剂型别名
                    resultStr = GetDOOT_List(codeValue, startDate, endDate);
                    break;
                case "SHLS": //诊疗目录
                    resultStr = GetSHLS_List(codeValue, startDate, endDate);
                    break;
                case "STSP": //服务诊疗匹配
                    resultStr = GetSTSP_List(codeValue, startDate, endDate);
                    break;
                case "DCDC": //药品分类
                    resultStr = GetDCDC_List(codeValue, startDate, endDate);
                    break;
                default:
                    break;
            }
            return Json(resultStr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  获取医院码
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetHPHP_List(string codeValue, string startDate, string endDate)
        {
            string targetOption ="<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>医院编号</th><th>医院名称</th><th>医院地址</th><th>编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>HP4264312612</td><td>东方儿童医院</td><td>上海浦东新区东方路666号</td><td>15646865</td></tr>";
            targetOption += "</tbody>";

            return targetOption;
        }

        /// <summary>
        /// 获取诊断码
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetDADA_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>诊断码</th><th>诊断码名称</th><th>诊断码地址</th><th>诊断码编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";

            return targetOption;
        }

        /// <summary>
        /// 获取 诊疗码
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetSPSP_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>诊疗码</th><th>诊断码名称</th><th>诊疗码地址</th><th>诊疗码编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";

            return targetOption;
        }

        /// <summary>
        ///  获取 药品码
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetSPCT_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>药品码</th><th>药品码名称</th><th>药品码地址</th><th>药品码编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        /// 获取 药品适应症
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetDIDI_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>药品适应症</th><th>药品适应症名称</th><th>药品适应症地址</th><th>药品适应症编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        /// 获取  药品码别名
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetDNDN_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>药品码别名</th><th>药品码别名名称</th><th>药品码别名地址</th><th>药品码别名编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        /// 获取 诊疗码别名
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetSPON_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>诊疗码别名</th><th>诊疗码别名名称</th><th>诊疗码别名地址</th><th>诊疗码别名编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        ///  获取  剂型码
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetDODO_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>剂型码</th><th>剂型码名称</th><th>剂型码地址</th><th>剂型码编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        /// 获取 剂型别名
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetDOOT_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>剂型别名</th><th>剂型别名名称</th><th>剂型别名地址</th><th>剂型别名编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        /// 获取 诊疗目录
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetSHLS_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>诊疗目录</th><th>诊疗目录名称</th><th>诊疗目录地址</th><th>诊疗目录编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        /// 获取 服务诊疗匹配
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetSTSP_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>药品分类</th><th>药品分类名称</th><th>药品分类地址</th><th>药品分类编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }

        /// <summary>
        ///  获取 药品分类
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetDCDC_List(string codeValue, string startDate, string endDate)
        {
            string targetOption = "<colgroup><col width = '50' ><col width = '150' ><col width = '150'><col width = '200'><col></colgroup>";
            targetOption += "<thead>";
            targetOption += "<tr><th><input type='checkbox' name='' lay-skin='primary' lay-filter='allChoose'></th><th>药品分类</th><th>药品分类名称</th><th>药品分类地址</th><th>药品分类编号</th></tr>";
            targetOption += "</thead>";
            targetOption += "<tbody>";
            targetOption += "<tr><td><input type = 'checkbox' name = '' lay-skin = 'primary'></td><td>SP235235</td><td>正常</td><td>正常号码</td><td>951615515</td></tr>";
            targetOption += "</tbody>";
            return targetOption;
        }
        #endregion
    }
}