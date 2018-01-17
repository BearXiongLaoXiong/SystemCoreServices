using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class HPHP
    {
        public HPHP(SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC_RESULT hphp)
        {
            HPHP_ID = hphp.HPHP_ID;
            HPHP_Name = hphp.HPHP_NAME;
            HPHP_Address = hphp.HPHP_ADDR;
            HPHP_No = hphp.HPHP_ACCT_NO;
        }
        public string HPHP_ID { get; set; }
        public string HPHP_Name { get; set; }
        public string HPHP_Address { get; set; }
        public string HPHP_No { get; set; }
    }
}
