using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_USUS_EMAIL_SELECT_ISACTIVE
    {
        public string pPolicy_NO { get; set; }
        public string pCert_No { get; set; }
    }

    public class SPEH_USUS_EMAIL_ISACTIVE_RESULT
    {
        public string MEME_KY { get; set; }
        public string USUS_ID { get; set; }
        public string USUS_EMAIL { get; set; }
        public string USUS_EMAIL_ISACTIVE { get; set; }
        public string Policy_No { get; set; }
    }
}