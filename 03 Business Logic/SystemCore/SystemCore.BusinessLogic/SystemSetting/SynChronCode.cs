using BusinessLogicRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCore.BusinessLogic.ISystemSetting;
using SystemCore.Entities.SystemSetting;

namespace SystemCore.BusinessLogic
{
    public class SynChronCode : ISynChronCode
    {
        private readonly ICommonBl _commonBl = new CommonBl();
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

        public SPEH_HPHP_HOSPITAL_INFO_WEB_LIST_RESULT GetHPHPByID(string hphid)
        {
            SPEH_HPHP_HOSPITAL_INFO_WEB_LIST entity = new SPEH_HPHP_HOSPITAL_INFO_WEB_LIST()
            {
                pHPHP_ID = hphid
            };

            return _commonBl.QuerySingle<SPEH_HPHP_HOSPITAL_INFO_WEB_LIST, SPEH_HPHP_HOSPITAL_INFO_WEB_LIST_RESULT>(entity).First();
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
        public string InsertCode(string[] TargetArray, string[] CodeArray)
        {
            foreach (string target in TargetArray) // 循环目标地址
            {
                // 找出 目标环境，然后修改目标地址
                SPEH_DASY_SYNC_CODE_LIST_RESULT baseStr = GetSynChronTarget()?.Where(x => x.Id.Equals(target))?.First();
                if (baseStr == null) continue;
                foreach (var code in CodeArray)
                {
                    // 找出 code 具体所有详细信息
                    var singcode = GetHPHPByID(code);
                    if (baseStr.Comment.Contains("WorkFlow"))
                    {

                        SPEH_DASY_DATA_SYNC_INSERT entityDasy = new SPEH_DASY_DATA_SYNC_INSERT()
                        {
                            ConnectionString = baseStr.DataBaseStr,
                            //pDASY_KY = singcode.DASY_KY,
                            ////pDASY_DTM = coder.DASY_DTM,
                            //pDASY_TYPE = singcode.DASY_TYPE,
                            //pDASY_OPRT = singcode.DASY_OPRT,
                            //pDASY_ID = singcode.DASY_ID,
                            pDASY_OPRT_USER = ""
                        };

                        //TypeDescriptor.AddAttributes(typeof(SPEH_DASY_DATA_SYNC_INSERT), new DatabaseConnectionAttribute(singcode.DataBaseStr));
                        _commonBl.Execute(entityDasy);

                        // 同步医院的增量 或者 诊疗的增量
                        TMP_HPHP_INSERT hphp = new TMP_HPHP_INSERT() { pHPHP_ID = code };
                    }

                    //TypeDescriptor.AddAttributes(typeof(SPEH_HPHP_HOSPITAL_INFO_INSERT), new DatabaseConnectionAttribute(singcode.DataBaseStr));
                    SPEH_HPHP_HOSPITAL_INFO_INSERT entityHPHP = new SPEH_HPHP_HOSPITAL_INFO_INSERT()
                    {
                        ConnectionString = baseStr.DataBaseStr,
                        pBKBK_ID = singcode.BKBK_ID,
                        pENTT_LANG_ID1 = singcode.ENTT_LANG_ID1,
                        pENTT_LANG_ID2 = singcode.ENTT_LANG_ID2,
                        pENTT_LANG_ID3 = singcode.ENTT_LANG_ID3,
                        pHPHP_ACCT_CONFM_DT = singcode.HPHP_ACCT_CONFM_DT,
                        pHPHP_ACCT_NAME = singcode.HPHP_ACCT_NAME,
                        pHPHP_ACCT_NO = singcode.HPHP_ACCT_NO,
                        pHPHP_ADDR = singcode.HPHP_ADDR,
                        pHPHP_ADDR_ENG = singcode.HPHP_ADDR_ENG,
                        pHPHP_CONTACT_NAME = singcode.HPHP_CONTACT_NAME,
                        pHPHP_EFT_IND = singcode.HPHP_EFT_IND,
                        pHPHP_EMAIL = singcode.HPHP_EMAIL,
                        pHPHP_FAX = singcode.HPHP_FAX,
                        pHPHP_FRN_SCCT_ID = singcode.HPHP_FRN_SCCT_ID,
                        pHPHP_ID = singcode.HPHP_ID,
                        pHPHP_NAME = singcode.HPHP_NAME,
                        pHPHP_NAME_FST = singcode.HPHP_NAME_FST,
                        pHPHP_NAME_FUL = singcode.HPHP_NAME_FUL,
                        pHPHP_NHI = singcode.HPHP_NHI,
                        pHPHP_OTH_NAME = singcode.HPHP_OTH_NAME,
                        pHPHP_PAY_HOLD_DT = singcode.HPHP_PAY_HOLD_DT,
                        pHPHP_PHONE = singcode.HPHP_PHONE,
                        pHPHP_PREAUTH_IND = singcode.HPHP_PREAUTH_IND,
                        pHPHP_SCCT_ID = singcode.HPHP_SCCT_ID,
                        pHPHP_SHIP_IND = singcode.HPHP_SHIP_IND,
                        pHPHP_WEBSIT = singcode.HPHP_WEBSIT,
                        pHPHP_ZIP = singcode.HPHP_ZIP,
                        pHPPN_ID = singcode.HPPN_ID,
                        //pREF_HPHP_ID = singcode.REF_HPHP_ID,
                        //pREF_IND = singcode.REF_IND,
                        pSHSH_KY = singcode.SHSH_KY,
                        pSYSV_BKBK_TYPE = singcode.SYSV_BKBK_TYPE,
                        pSYSV_HPHP_CLASS = singcode.SYSV_HPHP_CLASS,
                        pSYSV_HPHP_CL_STS = singcode.SYSV_HPHP_CL_STS,
                        pSYSV_HPHP_SUB_CLASS = singcode.SYSV_HPHP_SUB_CLASS,
                        pSYSV_HPHP_TYPE = singcode.SYSV_HPHP_TYPE,
                        pTAX_ID = singcode.TAX_ID,
                        pHPHP_COMMENT = singcode.HPHP_COMMENT
                    };
                    _commonBl.Execute(entityHPHP);
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
