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

        [SqlParameter(255)]
        public string pPassword { get; set; }

        [SqlParameter(0,ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(555, ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }
    }

    public class SPEH_USUS_USER_PWD_INFO_SELECT_RESULT
    {
        public string USUS_ID { get; set; }
        public string USUS_PSWD { get; set; }
        public string ENTT_DPT_ID { get; set; }
        public string ENTT_LANG_ID { get; set; }
        public string USUS_TITLE { get; set; }
        public string USUS_NAME { get; set; }
        public string SYSV_CERT_TYPE { get; set; }
        public string USUS_CERT_ID_NUM { get; set; }
        public string DOB { get; set; }
        public string USUS_EMAIL { get; set; }
        public string USUS_EMAIL_ISACTIVE { get; set; }
        public string USUS_SIGNUP_ISACTIVE { get; set; }
        public string USUS_CPHON_NUM { get; set; }
        public string USUS_WORK_PHON { get; set; }
        public string USUS_ADDR { get; set; }
        public string USUS_CITY { get; set; }
        public string USUS_ST { get; set; }
        public string USUS_ZIP { get; set; }
        public string USUS_EFF_DT { get; set; }
        public string USUS_END_DT { get; set; }
        public string USUS_KEY_TYPE { get; set; }
        public string USUS_KY { get; set; }
        public string USUS_NAME_FST { get; set; }
        public string USUS_NAME_FUL { get; set; }
        public string USUS_COMMENT { get; set; }
    }
}