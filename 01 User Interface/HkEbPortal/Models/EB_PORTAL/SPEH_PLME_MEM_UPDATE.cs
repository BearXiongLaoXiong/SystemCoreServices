using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_PLME_MEM_UPDATE
    {
        public int pPLPL_KY { get; set; }
        public int pMEME_KY { get; set; }
        public string pPDCT_ID { get; set; }
        public string pPDPD_ID { get; set; }
        public string pCMT_IND => "Y";
        public string pEHUSER { get; set; }
        [SqlParameter(555, ParameterDirection.Output)] public string pRTN_MSG { get; set; }
        [SqlParameter(direction: ParameterDirection.ReturnValue)] public int ReturnValue { get; set; }
    }
}