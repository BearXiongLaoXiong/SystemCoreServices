using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB2
    {
        public string pEHUSER { get; set; }
        public string pSYSV_PLPL_STS { get; set; }
        public string pPLPL_KY { get; set; }
        public string pPDCT_ID { get; set; }
        public string lang { get; set; }
        public string pMEME_KY { get; set; }
    }

    public class SPEH_PLME_PLOCY_MEME_INFO_LIST_WEB_RESULT4
    {
        public string MEME_KY { get; set; }
        public string MEME_NAME { get; set; }
        public string PLPL_KY { get; set; }
        public string PLPL_ID { get; set; }
        public string PDPD_ID { get; set; }
        public string PDCT_ID { get; set; }
        public string PDCT_NAME { get; set; }
        public string PDPD_NAME { get; set; }
        public string PDPD_LEVEL { get; set; }
        public string PLPD_DEF_IND { get; set; }
        public string DFF_IND { get; set; }
        public string PLME_AMT { get; set; }
        public string EMP_AMT { get; set; }
        public string EMPYEE_AMT { get; set; }
        public string TXTX_AMT { get; set; }
        public string SELECTED { get; set; }
        public string PLGP_PATH { get; set; }
    }
}