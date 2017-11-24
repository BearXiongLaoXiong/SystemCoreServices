using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_FAIMLY_AUTHORIZE
    {
        public int @pUSUS_KY { get; set; }
        public int pMEME_KY { get; set; }

        [SqlParameter(direction: ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }
    }
}