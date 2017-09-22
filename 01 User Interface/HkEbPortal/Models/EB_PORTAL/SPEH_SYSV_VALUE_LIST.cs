using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_SYSV_VALUE_LIST
    {
        public string pENTT_KY { get; set; }
        public string pSYSV_ENTITY { get; set; }
        public string pSYSV_TYPE { get; set; }
        public string pEHUSER { get; set; }
        public string lang { get; set; }
    }

    public class SPEH_SYSV_VALUE_LIST_RESULT
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}