using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    ///  报销管理 delete
    /// </summary>
    public class SPEH_CLIV_CLAIM_INVOICE_INFO_DELETE
    {
        public string pCLIV_KY { get; set; }

        [SqlParameter(15)]
        public string pEHUSER { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(555, direction:ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }

        public string pERR_CD { get; set; }

    }
}