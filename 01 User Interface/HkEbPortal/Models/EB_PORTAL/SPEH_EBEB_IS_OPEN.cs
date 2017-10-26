using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_EBEB_IS_OPEN
    {
        public int pMEME_KY { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public int pIsOpen { get; set; } = 0;
        [SqlParameter(555, direction: ParameterDirection.Output)] public string pRTN_MSG { get; set; }
        [SqlParameter(direction: ParameterDirection.ReturnValue)] public int ReturnValue { get; set; }
    }
}