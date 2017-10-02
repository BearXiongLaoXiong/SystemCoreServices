using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_USUS_USER_PWD_INFO_SELECT
    {
        [SqlParameter(255)]
        public string pPLPL_NO { get; set; }

        [SqlParameter(255)]
        public string pMEME_ID { get; set; }

        [SqlParameter(0,ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(555, ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }
    }

    public class SPEH_USUS_USER_PWD_INFO_SELECT_RESULT
    {
        public string GPGP_KY { get; set; }
        public string FMFM_KY { get; set; }
        public string MEME_KY { get; set; }
        public string Policy_No { get; set; }
        public string Cert_No { get; set; }
        public string Cert_Prefix { get; set; }
        public string DTM { get; set; }
    }
}