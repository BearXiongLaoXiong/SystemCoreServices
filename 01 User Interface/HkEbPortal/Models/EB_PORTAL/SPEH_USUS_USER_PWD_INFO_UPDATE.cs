using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_USUS_USER_PWD_INFO_UPDATE
    {
        public string pUSUS_ID { get; set; }
        public string pPassword { get; set; }
        public string pConfirmPassword { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(555,direction: ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }
    }
}