using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_FMAD_ACCOUNT_DET_LIST
    {
        public string pFMAC_KY { get; set; }

        public string pEHUSER { get; set; }
    }

    public class SPEH_FMAD_ACCOUNT_DET_LIST_RESULT
    {
        public string FMAC_KY { get; set; }
        public string Transection_Date { get; set; }
        public string Transection_Type { get; set; }
        public string Points { get; set; }
        public string Current_Points { get; set; }
        public string Comment { get; set; }
    }
}