﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_AQHT_HTML_INFO_INSERT
    {
        public int pMEME_KY { get; set; }
        public int pPLPL_KY { get; set; }
        [SqlParameter(20)] public string pPDPD_ID { get; set; }
        public string pEHUSER { get; set; }
        [SqlParameter(555,direction: ParameterDirection.Output)] public string pRTN_MSG { get; set; }
        [SqlParameter(direction: ParameterDirection.ReturnValue)] public int ReturnValue { get; set; }
    }
}