using System;
using System.Collections.Generic;
using System.Data;
using System.Framework.Aop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    [DatabaseConnection(ConnectionEnum.CustomizeConnectionString,typeof(SPEH_DASY_DATA_SYNC_INSERT))]
    public class SPEH_DASY_DATA_SYNC_INSERT: ICustomizeConnectionString
    {
        public string pDASY_KY { get; set; }

        public string pDASY_DTM { get; set; }

        public string pDASY_TYPE { get; set; }

        public string pDASY_OPRT { get; set; }

        public string pDASY_ID { get; set; }

        public string pDASY_OPRT_USER { get; set; }

        [SqlParameter(direction: ParameterDirection.Output)]
        public int pRTN_CD { get; set; }

        [SqlParameter(555, ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }

        public string ConnectionString { get; set; }
    }
}