using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_FMFM_LOGIN
    {
        public string @pPOLICYNO { get; set; }
        public string pUSUS_ID { get; set; }
        public string pUSUS_PSWD { get; set; }

        [SqlParameter(555, direction: ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }

        [SqlParameter(direction: ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }
    }

    public class UserInfo
    {
        public string USUS_ID { get; set; }
        public string USUS_KEY_TYPE { get; set; }
        public string USUS_KY { get; set; }
        public string USUS_EMAIL { get; set; }
        public string USUS_EMAIL_ISACTIVE { get; set; }
        public string USUS_FIRST_ISACTIVE { get; set; }
        public string USUS_INFO_IS_CONFIRM { get; set; }
        public string PFPF_ID { get; set; }
        public string ENTT_CLIENT_CD { get; set; }
        public string ENTT_KY { get; set; }
        public string ENTT_NAME { get; set; }
        public string GPPN_KY { get; set; }
        public string GPPN_NAME { get; set; }
        public string GPGP_KY { get; set; }
        public string GPGP_NAME { get; set; }
        public string FMFM_KY { get; set; }
        public string NAME { get; set; }
        public string INTIAL_AMT { get; set; }
        public string FMFM_CUR_AMT { get; set; }
        public string GCGC_KY { get; set; }
        public string GCGC_DESC { get; set; }
        public string TAX_CUR_AMT { get; set; }
        public string MEME_KY { get; set; }
        public string MEME_NAME { get; set; }
        public string MEME_CERT_ID_NUM { get; set; }
        public string ENTT_LANG_ID { get; set; }
    }
}