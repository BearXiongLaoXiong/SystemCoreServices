using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_USUS_EMAIL_ISACTIVE_UPDATE
    {
        public string pPolicy_NO { get; set; }
        public string pCert_No { get; set; }
        public string pPassWord { get; set; }
        [SqlParameter(direction: ParameterDirection.ReturnValue)] public int ReturnValue { get; set; }
    }
}