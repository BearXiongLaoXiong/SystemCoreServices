using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Ftp.Entities
{
    public class BearTest
    {
        public List<SqlDataRecord> pTKTK_KY { get; set; }

        public string test { get; set; }

        [SqlParameter(20, direction: ParameterDirection.Output)]
        public string outpar { get; set; }

        [SqlParameter(direction: ParameterDirection.ReturnValue)]
        public int ret { get; set; }
    }

    public class IMIM_PATH
    {
        public string IMIM_PATH_NAME { get; set; }
    }


    public class IMIM_STTS_UPDATE
    {
        public int IMIM_KY { get; set; }
    }
}
