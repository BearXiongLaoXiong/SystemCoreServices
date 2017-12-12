using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_PLFM_DET_LIST
    {
        public string pPLPL_KY { get; set; }
        public string pMEME_KY { get; set; }
        public string pEHUSER { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(255, direction: ParameterDirection.Output)]
        public string pERR_CD { get; set; }

        [SqlParameter(555, direction: ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }
    }

    public class SPEH_PLFM_DET_LIST_RESULT
    {
        public string PDCT_LONG_NAME { get; set; }
        public string PDPD_LONG_NAME { get; set; }
        public string PLME_AMT { get; set; }
        public string PDPD_LEVEL { get; set; }
        public string PDPD_LINK { get; set; }
    }
}