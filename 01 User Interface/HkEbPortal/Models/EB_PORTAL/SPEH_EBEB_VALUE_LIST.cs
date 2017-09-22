using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    ///  福利
    /// </summary>
    public class SPEH_EBEB_VALUE_LIST
    {
        public string pGPGP_KY { get; set; }
        public string pEHUSER { get; set; }
        public string lang { get; set; }
    }

    public class SPEH_EBEB_VALUE_LIST_RESULT
    {
        public string EBEB_KY { get; set; }
        public string EBEB_DESC { get; set; }
    }
}