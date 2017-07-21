using BusinessLogicRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCore.Entities.SystemSetting;

namespace SystemCore.BusinessLogic
{
    public class SynChronCode
    {
        private readonly ICommonBl _commonBl = new CommonBl();
        private List<SPEH_DASY_SYNC_CODE_LIST_RESULT> _cacheData = null;
        #region  获取同步目标环境
        /// <summary>
        ///  获取同步内容
        /// </summary>
        public List<SPEH_SYSV_VALUE_LIST_RESULT> GetSynChronContent()
        {
            SPEH_SYSV_VALUE_LIST entity = new SPEH_SYSV_VALUE_LIST()
            {
                pSYSV_ENTITY = "CDAD",
                pSYSV_TYPE = "CD_TYPE"
            };

            return _commonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(entity);
        }
        #endregion

        #region 获取同步目标内容
        /// <summary>
        /// 获取同步目标内容
        /// </summary>
        /// <returns></returns>
        public List<SPEH_DASY_SYNC_CODE_LIST_RESULT> GetSynChronTarget()
        {
            SPEH_DASY_SYNC_CODE_LIST entity = new SPEH_DASY_SYNC_CODE_LIST() { };

            return _commonBl.QuerySingle<SPEH_DASY_SYNC_CODE_LIST, SPEH_DASY_SYNC_CODE_LIST_RESULT>(entity);
        }
        #endregion

        #region 查询码值（包含所有）
        #region 获取 医院码
        /// <summary>
        ///  获取医院码
        /// </summary>
        /// <param name="synchronContent">同步内容</param>
        /// <param name="source">数据来源</param>
        /// <param name="codeValue">码值</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">截至日期</param>
        /// <returns></returns>
        public List<SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC_RESULT> GetHPHPCodeByCondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC entity = new SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };
            return _commonBl.QuerySingle<SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC, SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 诊断码
        /// <summary>
        /// 获取 诊断码
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_DADA_DIAGNOSIS_INFO_LIST_SYNC_RESULT> GetDADACodeByCondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_DADA_DIAGNOSIS_INFO_LIST_SYNC entity = new SPEH_DADA_DIAGNOSIS_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_DADA_DIAGNOSIS_INFO_LIST_SYNC, SPEH_DADA_DIAGNOSIS_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 诊疗码
        /// <summary>
        ///  获取诊疗码
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_SPSP_DIAGNOSIS_INFO_LIST_SYNC_RESULT> GetSPSPCodeByCondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_SPSP_DIAGNOSIS_INFO_LIST_SYNC entity = new SPEH_SPSP_DIAGNOSIS_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_SPSP_DIAGNOSIS_INFO_LIST_SYNC, SPEH_SPSP_DIAGNOSIS_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 药品码
        /// <summary>
        ///  获取 药品码
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_SPCT_INFO_LIST_SYNC_RESULT> GetSPCTCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_SPCT_INFO_LIST_SYNC entity = new SPEH_SPCT_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_SPCT_INFO_LIST_SYNC, SPEH_SPCT_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 药品适应症
        /// <summary>
        /// 获取 药品适应症
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_DIDI_INFO_LIST_SYNC_RESULT> GetDIDICodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_DIDI_INFO_LIST_SYNC entity = new SPEH_DIDI_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_DIDI_INFO_LIST_SYNC, SPEH_DIDI_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 药品码别名
        /// <summary>
        /// 获取 药品码别名
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_DNDN_INFO_LIST_SYNC_RESULT> GetDNDNCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_DNDN_INFO_LIST_SYNC entity = new SPEH_DNDN_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_DNDN_INFO_LIST_SYNC, SPEH_DNDN_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 诊疗码别名
        /// <summary>
        /// 获取 诊疗码别名
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_SPON_INFO_LIST_SYNC_RESULT> GetSPONCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_SPON_INFO_LIST_SYNC entity = new SPEH_SPON_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_SPON_INFO_LIST_SYNC, SPEH_SPON_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 剂型码
        /// <summary>
        /// 获取 剂型码
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_DODO_INFO_LIST_SYNC_RESULT> GetDODOCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_DODO_INFO_LIST_SYNC entity = new SPEH_DODO_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_DODO_INFO_LIST_SYNC, SPEH_DODO_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 剂型别名
        /// <summary>
        /// 获取 剂型别名
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_DOOT_INFO_LIST_SYNC_RESULT> GetDOOTCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_DOOT_INFO_LIST_SYNC entity = new SPEH_DOOT_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_DOOT_INFO_LIST_SYNC, SPEH_DOOT_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 诊疗目录
        /// <summary>
        /// 获取 诊疗目录
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_SHLS_INFO_LIST_SYNC_RESULT> GetSHLSCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_SHLS_INFO_LIST_SYNC entity = new SPEH_SHLS_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_SHLS_INFO_LIST_SYNC, SPEH_SHLS_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 服务诊疗匹配
        /// <summary>
        /// 获取 服务诊疗匹配
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_STSP_INFO_LIST_SYNC_RESULT> GetSTSPCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_STSP_INFO_LIST_SYNC entity = new SPEH_STSP_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_STSP_INFO_LIST_SYNC, SPEH_STSP_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion

        #region 获取 药品分类
        /// <summary>
        /// 获取 药品分类
        /// </summary>
        /// <param name="synchronContent"></param>
        /// <param name="source"></param>
        /// <param name="codeValue"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SPEH_DCDC_INFO_LIST_SYNC_RESULT> GetDCDCCodeBycondition(string synchronContent, string source, string codeValue, string startDate, string endDate)
        {
            SPEH_DCDC_INFO_LIST_SYNC entity = new SPEH_DCDC_INFO_LIST_SYNC()
            {
                pDASY_TYPE = synchronContent,
                pCODE = codeValue,
                pDASY_START_DATE = startDate,
                pDASY_END_DATE = endDate
            };

            return _commonBl.QuerySingle<SPEH_DCDC_INFO_LIST_SYNC, SPEH_DCDC_INFO_LIST_SYNC_RESULT>(entity);
        }
        #endregion
        #endregion


        /// <summary>
        /// 设置obj_T对象AType类型标签Attribute属性中的值
        /// </summary>
        /// <typeparam name="AType">标签类型</typeparam>
        /// <param name="obj_T">类</param>
        /// <param name="aTypePropertyName">标签属性名称</param>
        /// <returns>值</returns>
        public static void SetObjAttriVal<AType>(object obj_T, string aTypePropertyName, string nTypePropertyName)
        {
            Type typeT = obj_T.GetType();
            Type typeA = typeof(AType);
            object[] memberInfo = typeT.GetCustomAttributes(typeA, true);
            if (null != memberInfo && memberInfo.Length > 0)
            {
                System.Reflection.MemberInfo[] memberInfosAttr = typeA.GetMember(aTypePropertyName);
                if (null != memberInfosAttr && memberInfosAttr.Length > 0 && memberInfosAttr[0].MemberType == System.Reflection.MemberTypes.Property)
                {
                    System.Reflection.PropertyInfo propertyInfo = memberInfosAttr[0] as System.Reflection.PropertyInfo;
                    propertyInfo.SetValue(memberInfo[0], nTypePropertyName, null);
                }
            }
        }


        #region 同步码值（包含所有）
        #region  同步 医院码
        /// <summary>
        /// 同步 医院码
        /// </summary>
        /// <param name="TargetArray"></param>
        /// <param name="CodeArray"></param>
        /// <param name="data"></param>
        public string InsertHPHP(string[] TargetArray, string[] CodeArray, List<SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC_RESULT> data)
        {
            foreach (string target in TargetArray)
            {
                switch (target)
                {
                    case "1":  // 初审-复星(永安)生产
                        foreach (SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC_RESULT item in data)
                        {
                            // 找出 目标环境，然后修改目标地址
                            List<SPEH_DASY_SYNC_CODE_LIST_RESULT> baseStr = GetSynChronTarget();
                            SPEH_DASY_SYNC_CODE_LIST_RESULT first = baseStr?.Where(x => x.Id.Equals(target))?.First();
                            if (CodeArray.Contains(item.SEQ))
                            {

                                SPEH_DASY_DATA_SYNC_INSERT entityDasy = new SPEH_DASY_DATA_SYNC_INSERT()
                                {
                                    pDASY_KY = item.DASY_KY,
                                    //pDASY_DTM = coder.DASY_DTM,
                                    pDASY_TYPE = item.DASY_TYPE,
                                    pDASY_OPRT = item.DASY_OPRT,
                                    pDASY_ID = item.DASY_ID,
                                    pDASY_OPRT_USER = ""
                                };

                                TypeDescriptor.AddAttributes(typeof(SPEH_DASY_DATA_SYNC_INSERT), new DatabaseConnectionAttribute(first.DataBaseStr));
                                _commonBl.Execute(entityDasy);

                                // 同步医院的增量 或者 诊疗的增量
                                TMP_HPHP_INSERT hphp = new TMP_HPHP_INSERT() { pHPHP_ID = item.HPHP_ID };
                            }

                            TypeDescriptor.AddAttributes(typeof(SPEH_HPHP_HOSPITAL_INFO_INSERT), new DatabaseConnectionAttribute(first.DataBaseStr));
                            SPEH_HPHP_HOSPITAL_INFO_INSERT entityHPHP = new SPEH_HPHP_HOSPITAL_INFO_INSERT()
                            {
                                pBKBK_ID = item.BKBK_ID,
                                pENTT_LANG_ID1 = item.ENTT_LANG_ID1,
                                pENTT_LANG_ID2 = item.ENTT_LANG_ID2,
                                pENTT_LANG_ID3 = item.ENTT_LANG_ID3,
                                pHPHP_ACCT_CONFM_DT = item.HPHP_ACCT_CONFM_DT,
                                pHPHP_ACCT_NAME = item.HPHP_ACCT_NAME,
                                pHPHP_ACCT_NO = item.HPHP_ACCT_NO,
                                pHPHP_ADDR = item.HPHP_ADDR,
                                pHPHP_ADDR_ENG = item.HPHP_ADDR_ENG,
                                pHPHP_CONTACT_NAME = item.HPHP_CONTACT_NAME,
                                pHPHP_EFT_IND = item.HPHP_EFT_IND,
                                pHPHP_EMAIL = item.HPHP_EMAIL,
                                pHPHP_FAX = item.HPHP_FAX,
                                pHPHP_FRN_SCCT_ID = item.HPHP_FRN_SCCT_ID,
                                pHPHP_ID = item.HPHP_ID,
                                pHPHP_NAME = item.HPHP_NAME,
                                pHPHP_NAME_FST = item.HPHP_NAME_FST,
                                pHPHP_NAME_FUL = item.HPHP_NAME_FUL,
                                pHPHP_NHI = item.HPHP_NHI,
                                pHPHP_OTH_NAME = item.HPHP_OTH_NAME,
                                pHPHP_PAY_HOLD_DT = item.HPHP_PAY_HOLD_DT,
                                pHPHP_PHONE = item.HPHP_PHONE,
                                pHPHP_PREAUTH_IND = item.HPHP_PREAUTH_IND,
                                pHPHP_SCCT_ID = item.HPHP_SCCT_ID,
                                pHPHP_SHIP_IND = item.HPHP_SHIP_IND,
                                pHPHP_WEBSIT = item.HPHP_WEBSIT,
                                pHPHP_ZIP = item.HPHP_ZIP,
                                pHPPN_ID = item.HPPN_ID,
                                //pREF_HPHP_ID = item.REF_HPHP_ID,
                                //pREF_IND = item.REF_IND,
                                pSHSH_KY = item.SHSH_KY,
                                pSYSV_BKBK_TYPE = item.SYSV_BKBK_TYPE,
                                pSYSV_HPHP_CLASS = item.SYSV_HPHP_CLASS,
                                pSYSV_HPHP_CL_STS = item.SYSV_HPHP_CL_STS,
                                pSYSV_HPHP_SUB_CLASS = item.SYSV_HPHP_SUB_CLASS,
                                pSYSV_HPHP_TYPE = item.SYSV_HPHP_TYPE,
                                pTAX_ID = item.TAX_ID,
                                pHPHP_COMMENT = item.HPHP_COMMENT
                            };
                            _commonBl.Execute(entityHPHP);
                        }
                        break;
                    case "2":  // 核心-复星(永安)生产
                        break;
                    case "3":  // 初审-复星(永安)测试
                        break;
                    case "4":  // 核心-复星(永安)测试
                        break;
                    case "5":  // 初审-西安生产
                        break;
                    case "6":  // 核心-西安生产
                        break;
                    case "7":  // GCL初审开发环境
                        break;
                    case "8":  // GCL核心开发环境
                        break;
                    case "9":  // GCL试用环境
                        break;
                    case "10": // 泰国初审开发
                        break;
                    case "11": // 泰国核心开发
                        break;
                    case "12":// 泰国初审生产
                        break;
                    case "13": // 泰国核心生产
                        break;
                    case "14": // 中文新初审DEMO(兼太平开发)
                        break;
                    case "15": // 中文核心DEMO(兼太平开发)
                        break;
                    case "16": // 太平新初审UAT
                        break;
                    case "17": // 太平核心UAT
                        break;
                    case "18": // 太平新初审生产
                        break;
                    case "19": // 太平核心生产
                        break;
                    case "20": // 皓为新初审测试
                        break;
                    case "21": // 皓为核心测试
                        break;
                    case "22": // 皓为新初审生产
                        break;
                    case "23": // 皓为核心生产
                        break;
                    case "24": // 新初审UAT
                        break;
                    case "25": // 新核心UAT
                        break;
                    case "26": // 英文新初审DEMO
                        break;
                    case "27": // 英文核心DEMO
                        break;
                    case "28": // 云南初审开发
                        break;
                    case "29": // 云南核心开发
                        break;
                    case "30": // 丘博初审开发
                        break;
                    case "31": // 云南丘博核心开发
                        break;
                    default:
                        break;
                }
            }
            return "同步医院码成功！";
        }



        public void SelectAddress()
        {

        }
        #endregion

        #endregion
    }
}
