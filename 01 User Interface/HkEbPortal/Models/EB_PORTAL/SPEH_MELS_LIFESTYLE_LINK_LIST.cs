using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    /// 被保险人生活方式
    /// </summary>
    public class SPEH_MELS_LIFESTYLE_LINK_LIST
    {
        public string pMEME_KY { get; set; }
        public string pEHUSER { get; set; }
    }

    public class SPEH_MELS_LIFESTYLE_LINK_LIST_RESULT
    {
        public string MELS_KY{get;set;}  
        public string MEME_KY{get;set;}  
        public string MELS_EFF_DT{get;set;}  
        public string MELS_END_DT{get;set;}  
        public string SYSV_LSTY_CD{get;set;}  
        public string SYSV_LSTY_CD_DESC{get;set;} 
        public string MELS_COMMENT { get; set; }
    }
}