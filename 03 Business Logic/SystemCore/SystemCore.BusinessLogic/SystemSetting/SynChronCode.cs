using BusinessLogicRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCore.Entities.SystemSetting;

namespace SystemCore.BusinessLogic
{
    public class SynChronCode
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
    }
}
