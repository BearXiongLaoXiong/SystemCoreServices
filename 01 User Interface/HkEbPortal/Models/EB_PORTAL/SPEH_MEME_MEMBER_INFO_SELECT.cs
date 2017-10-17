using System;
using System.Collections.Generic;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_MEME_MEMBER_INFO_SELECT
    {
        [SqlParameter(255)]
        public string pMEME_KY { get; set; }
    }

    public class SPEH_MEME_MEMBER_INFO_SELECT_RESULT
    {
        public string MEME_KY { get; set; }
        public string GPGP_KY { get; set; }
        public string SGSG_KY { get; set; }
        public string FMFM_KY { get; set; }
        public string GCGC_KY { get; set; }
        public string SYSV_MEME_REL_CD { get; set; }
        public string MEME_NAME { get; set; }
        public string MEME_TITLE { get; set; }
        public string SYSV_MEME_STS { get; set; }
        public string MEME_ORIG_EFF_DT { get; set; }
        public string MEME_TERM_DT { get; set; }
        public string ENTT_LANG_ID { get; set; }
        public string MEME_CITIZEN { get; set; }
        public string SYSV_CERT_TYPE { get; set; }
        public string MEME_CERT_ID_NUM { get; set; }
        public string SYSV_SEX { get; set; }
        public string MEME_BIRTH_DT { get; set; }
        public string MEME_WRK_PHONE { get; set; }
        public string MEME_CEL_PHONE { get; set; }
        public string MEME_HOM_PHONE { get; set; }
        public string MEME_EMAIL { get; set; }
        public string SYSV_MARITAL_STATUS { get; set; }
        public string MEME_VIP_IND { get; set; }
        public string MEME_STDT_IND { get; set; }
        public string MEME_SMK_IND { get; set; }
        public string MEME_SHIP_IND { get; set; }
        public string MEME_DISABL_IND { get; set; }
        public string MEME_FAM_LINK_KY { get; set; }
        public string MEME_OCC_CLASS { get; set; }
        public string MEME_OCC_TYPE { get; set; }
        public string SYSV_ADDR_TYPE { get; set; }
        public string MEME_ADDR { get; set; }
        public string SCCT_ID { get; set; }
        public string MEME_ZIP { get; set; }
        public string MEME_LOC_CD { get; set; }
        public string MEME_CLIENT_ID { get; set; }
        public string MEME_APAY_BANK_ID { get; set; }
        public string SYSV_APAY_BKBK_TYPE { get; set; }
        public string MEME_APAY_ACCT_NO { get; set; }
        public string MEME_APAY_ACCT_NAME { get; set; }
        public string MEME_APAY_ACCT_CONFM_DT { get; set; }
        public string MEME_AREC_BANK_ID { get; set; }
        public string SYSV_AREC_BKBK_TYPE { get; set; }
        public string MEME_AREC_ACCT_NO { get; set; }
        public string MEME_AREC_ACCT_NAME { get; set; }
        public string MEME_AREC_ACCT_CONFM_DT { get; set; }
        public string MEME_NAME_FST { get; set; }
        public string MEME_NAME_FUL { get; set; }
    }
}