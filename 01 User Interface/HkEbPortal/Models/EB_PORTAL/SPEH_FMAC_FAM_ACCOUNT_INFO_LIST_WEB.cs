using System;
using System.Collections.Generic;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    /// 账单信息
    /// </summary>
    public class SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB
    {
        [SqlParameter(55)]
        public string pEHUSER { get; set; }
        [SqlParameter(55)]
        public string pFMFM_KY { get; set; }
    }

    public class SPEH_FMAC_FAM_ACCOUNT_INFO_LIST_WEB_RESULT
    {
        public string FMAC_KY { get; set; }
        public string PLPL_KY { get; set; }
        public string Policy_ID { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Initial_Amout { get; set; }
        public string Current_Points { get; set; }
        public string Comment { get; set; }

    }
}