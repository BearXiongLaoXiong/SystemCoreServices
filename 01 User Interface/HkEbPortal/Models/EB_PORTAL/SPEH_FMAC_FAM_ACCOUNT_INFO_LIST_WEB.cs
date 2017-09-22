using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    /// 账单信息
    /// </summary>
    public class SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB
    {
        public string pEHUSER { get; set; }
        public string lang { get; set; }
    }

    public class SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB_RESULT
    {
        public string FMAC_KY { get; set; }
        public string GPAC_KY { get; set; }
        public string FMFM_KY { get; set; }
        public string MEME_KY { get; set; }
        public string RVS_IND { get; set; }
        public string VOID_IND_DESC { get; set; }
        public string FMAC_DTM { get; set; }
        public string INV_DTM { get; set; }
        public string INV_AMT { get; set; }
        public string POST_TYPE { get; set; }
        public string POST_TYPE_DESC { get; set; }
        public string POST_KY { get; set; }
        public string POST_KY_DESC { get; set; }
        public string SYSV_BILL_FREQ { get; set; }
        public string SYSV_BILL_FREQ_DESC { get; set; }
        public string POST_AMT { get; set; }
        public string FMFM_CUR_AMT { get; set; }
        public string BEF_FMFM_AMT { get; set; }
        public string COMMENT { get; set; }
        public string MEME_NAME { get; set; }
        public string MEME_ADDR { get; set; }
        public string MEME_CEL_PHONE { get; set; }
        public string MEME_HOM_PHONE { get; set; }
        public string SYSV_CERT_TYPE { get; set; }
        public string MEME_CERT_ID_NUM { get; set; }
        public string INV_ID { get; set; }
        public string IVEM_PATH { get; set; }
    }
}