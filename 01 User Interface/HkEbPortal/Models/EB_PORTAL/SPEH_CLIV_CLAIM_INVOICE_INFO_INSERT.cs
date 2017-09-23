using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_CLIV_CLAIM_INVOICE_INFO_INSERT
    {
        [SqlParameter(255, direction: ParameterDirection.Output)]
        public string pCLIV_KY { get; set; }

        [SqlParameter(255)]
        public string pEBEB_KY { get; set; }

        [SqlParameter(255)]
        public string pFMFM_KY { get; set; }

        [SqlParameter(255)]
        public string pGPGP_KY { get; set; }

        [SqlParameter(255)]
        public string pGCGC_KY { get; set; }

        [SqlParameter(255)]
        public string pMEME_KY { get; set; }

        [SqlParameter(255)]
        public string pCLIV_ID { get; set; }

        [SqlParameter(255)]
        public string pSYSV_CLIV_TYPE { get; set; }

        [SqlParameter(255)]
        public string pSYSV_CLIV_STS { get; set; }

        [SqlParameter(255)]
        public string pCLIV_STS_RSN { get; set; }

        [SqlParameter(255)]
        public string pCLIV_STS_DTM { get; set; }

        [SqlParameter(255)]
        public string pCLIV_DT { get; set; }

        [SqlParameter(255)]
        public string pCLIV_APP_DT { get; set; }

        [SqlParameter(255)]
        public string pCLIV_FINL_DT { get; set; }

        [SqlParameter(255)]
        public string pCLIV_CHG { get; set; }

        [SqlParameter(255)]
        public string pCLIV_APPLY_AMT { get; set; }

        [SqlParameter(255)]
        public string pCLIV_ALLOW_AMT { get; set; }

        [SqlParameter(255)]
        public string pCLIV_ADJ_AMT { get; set; }

        [SqlParameter(255)]
        public string pCLIV_PAYABLE { get; set; }

        [SqlParameter(255)]
        public string pCLIV_ADJ_EXEX_ID { get; set; }

        [SqlParameter(255)]
        public string pCLIV_ADJ_USUS_ID { get; set; }

        [SqlParameter(255)]
        public string pCLIV_IMG_PATH { get; set; }

        [SqlParameter(15)]
        public string pCLIV_COMMENT { get; set; }

        [SqlParameter(255)]
        public string pEHUSER { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(555, direction: ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }
        
        public string pERR_CD { get; set; }

        [SqlParameter(10)]
        public string lang { get; set; }
    }
}