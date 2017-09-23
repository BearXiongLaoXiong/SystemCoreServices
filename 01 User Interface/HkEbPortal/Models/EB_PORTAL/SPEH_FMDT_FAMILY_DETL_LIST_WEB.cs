﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class SPEH_FMDT_FAMILY_DETL_LIST_WEB
    {
        public string pFMFM_KY { get; set; }
        public string pEHUSER { get; set; }
    }

    public class SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT
    {
        public string FMDT_KY { get; set; }
        public string FMFM_KY { get; set; }
        public string FMDT_EFF_DT { get; set; }
        public string FMDT_TERM_DT { get; set; }
        public string FMFM_ANNUAL_SALARY { get; set; }
        public string FMFM_DEPT { get; set; }
        public string GCGC_KY { get; set; }
        public string GCGC_DESC { get; set; }
        public string FMFM_POSITION { get; set; }
        public string FMDT_COMMENT { get; set; }
    }
}