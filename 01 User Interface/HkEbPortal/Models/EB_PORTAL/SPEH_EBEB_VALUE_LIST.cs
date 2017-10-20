using System;
using System.Collections.Generic;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    ///  福利
    /// </summary>
    public class SPEH_EBEB_VALUE_LIST
    {
        public string pMEME_KY { get; set; }

        [SqlParameter(55)]
        public string pEHUSER { get; set; }

        [SqlParameter(15)]
        public string lang { get; set; }
    }

    public class SPEH_EBEB_VALUE_LIST_RESULT
    {
        public string EBEB_KY { get; set; }
        public string EBEB_DESC { get; set; }
    }
}