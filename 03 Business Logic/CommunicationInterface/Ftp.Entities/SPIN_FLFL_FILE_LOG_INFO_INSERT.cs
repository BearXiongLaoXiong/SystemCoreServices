using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ftp.Entities
{
    //[DatabaseConnection("ss")]
    public class SPIN_FLFL_FILE_LOG_INFO_INSERT
    {
        [SqlParameter(255)] public string pFLFL_TYPE { get; set; }
        [SqlParameter(255)] public string pFILE_NAME { get; set; }
        [SqlParameter(255)] public string pFLFL_URL { get; set; }
        [SqlParameter(255)] public string pFLFL_STS { get; set; }
        [SqlParameter(255)] public string pFLFL_USUS_ID { get; set; }
        [SqlParameter(255, ParameterDirection.Output)] public string pFLFL_KY { get; set; }
        [SqlParameter(255, ParameterDirection.Output)] public string pRTN_MSG { get; set; }
        [SqlParameter(direction: ParameterDirection.ReturnValue)] public int ReturnValue { get; set; } = 999;
    }
}
