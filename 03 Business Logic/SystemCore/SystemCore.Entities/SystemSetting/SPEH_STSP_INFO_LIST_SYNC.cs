using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class SPEH_STSP_INFO_LIST_SYNC
    {
        public string pDASY_TYPE { get; set; }

        public string pCODE { get; set; }

        public string pDASY_START_DATE { get; set; }

        public string pDASY_END_DATE { get; set; }
    }

    public class SPEH_STSP_INFO_LIST_SYNC_RESULT
    {
        public string SEQ { get; set; }
        public string STSP_KY        {get;set;}
		public string STCV_PFX       {get;set;}
		public string SYSV_SPSP_IND  {get;set;}
		public string SYSV_CLIV_TYPE {get;set;}
		public string SPSP_ID_LOW    {get;set;}
		public string SPSP_ID_HIGH   {get;set;}
		public string STST_ID        {get;set;}
		public string DASY_KY        {get;set;}
		public string DASY_DTM       {get;set;}
		public string DASY_TYPE      {get;set;}
		public string DASY_OPRT      {get;set;}
		public string DASY_ID        {get;set;}
		public string DASY_OPRT_USER { get; set; }
    }
}
