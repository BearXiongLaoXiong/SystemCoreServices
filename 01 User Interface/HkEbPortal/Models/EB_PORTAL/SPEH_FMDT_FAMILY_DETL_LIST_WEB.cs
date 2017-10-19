using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Framework.Aop;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class SPEH_FMDT_FAMILY_DETL_LIST_WEB
    {
        [SqlParameter(255)]
        public string pFMFM_KY { get; set; }
        [SqlParameter(255)]
        public string pEHUSER { get; set; }
    }

    public class SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT
    {
        public string FMDT_KY { get; set; }
        public string FMFM_KY { get; set; }
        private string fmdt_end_dt;
        public string FMDT_EFF_DT {
            get {
                if (string.IsNullOrWhiteSpace(fmdt_end_dt))
                    return "";
                else
                    return Convert.ToDateTime(fmdt_end_dt).ToString("dd/MM/yyyy");
            }
            set { fmdt_end_dt = value; }
        }
        private string fmdt_term_dt;
        public string FMDT_TERM_DT {
            get
            {
                if (string.IsNullOrWhiteSpace(fmdt_term_dt))
                    return "";
                else
                    return Convert.ToDateTime(fmdt_term_dt).ToString("dd/MM/yyyy");
            }
            set { fmdt_term_dt = value; }
        }
        public string FMFM_ANNUAL_SALARY { get; set; }
        public string FMFM_DEPT { get; set; }
        public string GCGC_KY { get; set; }
        public string GCGC_DESC { get; set; }
        public string FMFM_POSITION { get; set; }
        public string FMDT_COMMENT { get; set; }
        public string TITLE { get; set; }
    }
}