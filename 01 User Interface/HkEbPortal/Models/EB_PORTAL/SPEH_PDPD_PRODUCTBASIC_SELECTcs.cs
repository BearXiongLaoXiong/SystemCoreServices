using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_PDPD_PRODUCTBASIC_SELECT
    {
        public string pPDPD_ID { get; set; }
    }

    public class SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT1
    {
        public string PDPD_ID { get; set; }
        public string PDPD_ID_DESC { get; set; }
        public string PDCT_EFF_DT { get; set; }
        public string PDCT_END_DT { get; set; }
        public string LBLB_LONG_NAME { get; set; }
        public string LBLB_ID { get; set; }
        public string PDPD_SHORT_NAME { get; set; }
        public string PDPD_LONG_NAME { get; set; }
        public string CLIENT_PROD_ID { get; set; }
        public string PDPD_SPC_TERM { get; set; }
        public string PDBD_LTLT_A_AMT { get; set; }
        public string PDBD_DEDE_D_AMT { get; set; }
        public string PDBD_DEDE_V_AMT { get; set; }
        public string PDBD_DEDE_A_AMT { get; set; }
        public string PDBD_LTLT_D_AMT { get; set; }
        public string PDBD_LTLT_V_AMT { get; set; }
        public string PDBD_WAIT_DAYS { get; set; }
        public string PDBD_LTLT_DAYS { get; set; }
        public string PDBD_LTLT_VSTS { get; set; }
        public string PDBD_COIN_PCT { get; set; }

    }

    public class SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT2
    {
        public string BFBF_ID { get; set; }
        public string BFBF_DESC { get; set; }
        public string PDBD_LTLT_A_AMT { get; set; }
        public string PDBD_DEDE_D_AMT { get; set; }
        public string PDBD_DEDE_V_AMT { get; set; }
        public string PDBD_DEDE_A_AMT { get; set; }
        public string PDBD_LTLT_D_AMT { get; set; }
        public string PDBD_LTLT_V_AMT { get; set; }
        public string PDBD_WAIT_DAYS { get; set; }
        public string PDBD_LTLT_DAYS { get; set; }
        public string PDBD_LTLT_VSTS { get; set; }
        public string PDBD_COIN_PCT { get; set; }

    }

    public class SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT3
    {
        public string TITLE { get; set; }
        public string TEXT { get; set; }
    }

    public class SPEH_PDPD_PRODUCTBASIC_SELECT_RESULT4
    {
        public string COLOR { get; set; }
        public string TITLE { get; set; }
        public string BUF1 { get; set; }
    }
}