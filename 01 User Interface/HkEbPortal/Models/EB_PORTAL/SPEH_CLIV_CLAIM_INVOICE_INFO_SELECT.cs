﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT
    {
        public string pCLIV_KY { get; set; }
    }

    public class SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT_RESULT
    {
        public string CLIV_KY { get; set; }
        public string EBEB_KY { get; set; }
        public string FMFM_KY { get; set; }
        public string FMFM_NAME { get; set; }
        public string GPGP_KY { get; set; }
        public string GPGP_NAME { get; set; }
        public string GCGC_KY { get; set; }
        public string MEME_KY { get; set; }
        public string CLIV_ID { get; set; }
        public string SYSV_CLIV_TYPE { get; set; }
        public string SYSV_CLIV_STS { get; set; }
        public string CLIV_STS_RSN { get; set; }
        public string CLIV_STS_DTM { get; set; }
        public string CLIV_DT { get; set; }
        public string CLIV_APP_DT { get; set; }
        public string CLIV_FINL_DT { get; set; }
        public string CLIV_CHG { get; set; }
        public string CLIV_APPLY_AMT { get; set; }
        public string CLIV_ALLOW_AMT { get; set; }
        public string CLIV_ADJ_AMT { get; set; }
        public string CLIV_PAYABLE { get; set; }
        public string CLIV_ADJ_EXEX_ID { get; set; }
        public string CLIV_ADJ_USUS_ID { get; set; }
        public string CLIV_IMG_PATH { get; set; }
        public string CLIV_COMMENT { get; set; }
    }
}