using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_USUS_IS_CONFIRM_UPDATE
    {
        public string pUSUS_ID { get; set; }

        [SqlParameter(1)] public string pUSUS_INFO_IS_CONFIRM { get; set; }

        [SqlParameter(direction: ParameterDirection.ReturnValue)] public int ReturnValue { get; set; }
    }
}