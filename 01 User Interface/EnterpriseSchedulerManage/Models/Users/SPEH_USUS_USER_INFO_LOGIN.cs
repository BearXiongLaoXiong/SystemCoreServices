using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Web;

namespace EnterpriseSchedulerManage.Models.Users
{
    public class SPEH_USUS_USER_INFO_LOGIN
    {
        public string pUSUS_NAME { get; set; }

        public string pUSUS_PSWD { get; set; }

        [SqlParameter(555, ParameterDirection.Output)]
        public string pUSUS_MSG { get; set; }
    }

    public class UserInfo
    {
        public string USUS_ID { get; set; }
        public string USUS_LOGIN { get; set; }
        public string ENTT_DPT_ID { get; set; }
        public string ENTT_LANG_ID { get; set; }
        public string USUS_TITLE { get; set; }
        public string USUS_NAME { get; set; }
        public string SYSV_CERT_TYPE { get; set; }
        public string USUS_CERT_ID_NUM { get; set; }
        public string USUS_EMAIL { get; set; }
        public string USUS_CPHON_NUM { get; set; }
        public string USUS_WORK_PHON { get; set; }
        public string USUS_ADDR { get; set; }
        public string USUS_CITY { get; set; }
        public string USUS_ST { get; set; }
        public string USUS_ZIP { get; set; }
        public string USUS_EFF_DT { get; set; }
        public string USUS_END_DT { get; set; }
        public string USUS_KEY_TYPE { get; set; }
        public string USUS_KY { get; set; }
        public string USUS_NAME_FST { get; set; }
        public string USUS_NAME_FUL { get; set; }
        public string USUS_LOGIN_IND { get; set; }
        public string USUS_LOGIN_LAST_DTM { get; set; }
        public string USUS_COMMENT { get; set; }
    }
}