using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_AQHT_HTML_INFO_INSERT
    {
        public int MEME_KY { get; set; }
        public int PLPL_KY { get; set; }
        public string PDPD_ID { get; set; }
        public string pEHUSER { get; set; }
        //[SqlParameter]
        public string pRTN_MSG { get; set; }
        public int ReturnValue { get; set; }
    }
}