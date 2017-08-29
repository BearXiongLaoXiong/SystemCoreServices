using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    [DatabaseConnection(ConnectionEnum.CustomizeConnectionString)]
    public class SPEH_HPHP_HOSPITAL_INFO_INSERT: ICustomizeConnectionString
    {
        [SqlParameter(255)]
        public string pHPHP_ID { get; set; }

        [SqlParameter(255)]
        public string pSYSV_HPHP_CLASS { get; set; }

        [SqlParameter(255)]
        public string pSYSV_HPHP_SUB_CLASS { get; set; }

        [SqlParameter(255)]
        public string pSYSV_HPHP_TYPE { get; set; }

        [SqlParameter(255)]
        public string pHPHP_SHIP_IND { get; set; }

        [SqlParameter(255)]
        public string pHPHP_NAME { get; set; }

        [SqlParameter(255)]
        public string pHPHP_OTH_NAME { get; set; }

        [SqlParameter(255)]
        public string pHPHP_NHI { get; set; }

        [SqlParameter(255)]
        public string pHPPN_ID { get; set; }

        [SqlParameter(255)]
        public string pTAX_ID { get; set; }

        [SqlParameter(255)]
        public string pENTT_LANG_ID1 { get; set; }

        [SqlParameter(255)]
        public string pENTT_LANG_ID2 { get; set; }

        [SqlParameter(255)]
        public string pENTT_LANG_ID3 { get; set; }

        [SqlParameter(255)]
        public string pSYSV_HPHP_CL_STS { get; set; }

        [SqlParameter(255)]
        public string pHPHP_PAY_HOLD_DT { get; set; }

        [SqlParameter(255)]
        public string pHPHP_PREAUTH_IND { get; set; }

        [SqlParameter(255)]
        public string pHPHP_EFT_IND { get; set; }

        [SqlParameter(255)]
        public string pHPHP_ADDR { get; set; }

        [SqlParameter(255)]
        public string pHPHP_ADDR_ENG { get; set; }

        [SqlParameter(255)]
        public string pHPHP_SCCT_ID { get; set; }

        [SqlParameter(255)]
        public string pHPHP_FRN_SCCT_ID { get; set; }

        [SqlParameter(255)]
        public string pHPHP_ZIP { get; set; }

        [SqlParameter(255)]
        public string pHPHP_EMAIL { get; set; }

        [SqlParameter(255)]
        public string pHPHP_WEBSIT { get; set; }

        [SqlParameter(255)]
        public string pHPHP_PHONE { get; set; }

        [SqlParameter(255)]
        public string pHPHP_FAX { get; set; }

        [SqlParameter(255)]
        public string pHPHP_CONTACT_NAME { get; set; }

        [SqlParameter(255)]
        public string pSHSH_KY { get; set; }

        [SqlParameter(255)]
        public string pBKBK_ID { get; set; }

        [SqlParameter(255)]
        public string pSYSV_BKBK_TYPE { get; set; }

        [SqlParameter(255)]
        public string pHPHP_ACCT_NO { get; set; }

        [SqlParameter(255)]
        public string pHPHP_ACCT_NAME { get; set; }

        [SqlParameter(255)]
        public string pHPHP_ACCT_CONFM_DT { get; set; }

        [SqlParameter(255)]
        public string pHPHP_NAME_FST { get; set; }

        [SqlParameter(255)]
        public string pHPHP_NAME_FUL { get; set; }

        [SqlParameter(255)]
        public string pREF_IND { get; set; }

        [SqlParameter(255)]
        public string pREF_HPHP_ID { get; set; }

        [SqlParameter(255)]
        public string pHPHP_COMMENT { get; set; }

        [SqlParameter(255)]
        public string pEHUSER { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(555, ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public DateTime pERR_CD { get; set; }

        public string ConnectionString { get; set; }
    }
}
