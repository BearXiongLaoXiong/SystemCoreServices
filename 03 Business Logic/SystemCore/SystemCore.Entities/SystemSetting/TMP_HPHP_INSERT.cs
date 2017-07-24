using System;
using System.Collections.Generic;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class TMP_HPHP_INSERT
    {
        [SqlParameter(255)]
        public string pHPHP_ID { get; set; }

        [SqlParameter(255)]
        public string pSPSP_ID { get; set; }
    }
}
