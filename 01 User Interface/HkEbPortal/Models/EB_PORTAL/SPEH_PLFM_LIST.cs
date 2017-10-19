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

        public string pERR_CD { get; set; }

        [SqlParameter(555, ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }
    }

    public class SPEH_PLFM_LIST_RESULT
    {
        public string PLPL_ID { get; set; }
        public string SYSV_PLPL_STS { get; set; }
        private string plme_str_dt;
        public string PLME_STR_DT {
            get
            {
                if (string.IsNullOrWhiteSpace(plme_str_dt))
                    return "";
                else
                    return Convert.ToDateTime(plme_str_dt).ToString("dd/MM/yyyy");
            }
            set { plme_str_dt = value; }
        }
        private string plpl_end_dt;
        public string PLPL_END_DT
        {
            get
            {
                if (string.IsNullOrWhiteSpace(plpl_end_dt))
                    return "";
                else
                    return Convert.ToDateTime(plpl_end_dt).ToString("dd/MM/yyyy");
            }
            set { plpl_end_dt = value; }
        }
        public string MEME_NAME { get; set; }
        public string SYSV_MEME_REL_CD { get; set; }
        public string Cert_No { get; set; }
        public string Cert_Prefix { get; set; }
        public string CLASS { get; set; }
    }
}