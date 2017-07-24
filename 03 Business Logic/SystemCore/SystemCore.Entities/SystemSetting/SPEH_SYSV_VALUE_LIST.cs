using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class SPEH_SYSV_VALUE_LIST
    {
        [SqlParameter(12)] public string pENTT_COMP_ID { get; set; }

        [SqlParameter(12)] public string pSYSV_ENTITY { get; set; }

        [SqlParameter(225)] public string pLANG_IND { get; set; }

        [SqlParameter(25)] public string pSYSV_TYPE { get; set; }

        [SqlParameter(25)] public string pEHUSER { get; set; }
    }

    public class SPEH_SYSV_VALUE_LIST_RESULT
    {
        public string SEQ { get; set; }

        public string value { get; set; }

        public string text { get; set; }
    }
}
