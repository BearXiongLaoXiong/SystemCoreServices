using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class HPHP
    {
        public HPHP() { }

        public HPHP(SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC_RESULT hphp)
        {
            HPHP_ID = hphp.HPHP_ID;
            HPHP_Name = hphp.HPHP_NAME;
            HPHP_Address = hphp.HPHP_ADDR;
            DASY_KY = hphp.DASY_KY;
            DASY_TYPE = hphp.DASY_TYPE;
            DASY_OPRT = hphp.DASY_OPRT;
            DASY_ID = hphp.DASY_ID;
            DASY_OPRT_USER = hphp.DASY_OPRT_USER;
        }

        public string HPHP_ID { get; set; }
        public string HPHP_Name { get; set; }
        public string HPHP_Address { get; set; }
        public string DASY_KY { get; set; }
        public string DASY_TYPE { get; set; }
        public string DASY_OPRT { get; set; }
        public string DASY_ID { get; set; }
        public string DASY_OPRT_USER { get; set; }

    }
}
