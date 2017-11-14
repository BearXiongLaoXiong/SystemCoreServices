using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_USUS_IS_CONFIRM_SELECT
    {
        public string pUSUS_ID { get; set; }

        [SqlParameter(1, ParameterDirection.Output)] public string pUSUS_INFO_IS_CONFIRM { get; set; }
    }
}