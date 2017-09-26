using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_PLFM_LIST
    {
        public string pFMFM_KY { get; set; }
        public string pMEME_KY { get; set; }

        [SqlParameter(15)]
        public string pEHUSER { get; set; }

        [SqlParameter(0,ParameterDirection.Output)]
        public int pRTN_CD { get; set; }
        public DateTime pERR_CD { get; set; }

        [SqlParameter(555, ParameterDirection.Output)]
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