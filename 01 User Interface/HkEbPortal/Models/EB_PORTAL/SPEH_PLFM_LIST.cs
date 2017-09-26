using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_PLFM_LIST
    {
        public string pFMFM_KY { get; set; }
        public string pMEME_KY { get; set; }
        public string pEHUSER { get; set; }
        public string pRTN_CD { get; set; }
        public string pERR_CD { get; set; }
        public string pRTN_MSG { get; set; }
    }

    public class SPEH_PLFM_LIST_RESULT
    {
        public string PLPL_ID { get; set; }
        public string SYSV_PLPL_STS { get; set; }
        public string PLME_STR_DT { get; set; }
        public string PLPL_END_DT { get; set; }
        public string MEME_NAME { get; set; }
        public string SYSV_MEME_REL_CD { get; set; }
        public string Cert_No { get; set; }
        public string Cert_Prefix { get; set; }
        public string CLASS { get; set; }
    }
}