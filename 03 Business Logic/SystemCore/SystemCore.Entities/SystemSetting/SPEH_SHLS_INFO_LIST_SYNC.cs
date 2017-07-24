using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class SPEH_SHLS_INFO_LIST_SYNC
    {
        public string pDASY_TYPE { get; set; }

        public string pCODE { get; set; }

        public string pDASY_START_DATE { get; set; }

        public string pDASY_END_DATE { get; set; }
    }

    public class SPEH_SHLS_INFO_LIST_SYNC_RESULT
    {
        public string SEQ { get; set; }
        public string SHLS_KY            {get;set;}
		public string SHSH_KY            {get;set;}
		public string SYSV_SVC_ID_TYPE   {get;set;}
		public string SHLS_PSPS_TYPE     {get;set;}
		public string SHLS_APPLY_TYPE    {get;set;}
		public string SHLS_LOW_ID        {get;set;}
		public string SHLS_HIGH_ID       {get;set;}
		public string SHLS_EFF_DT        {get;set;}
		public string SHLS_TERM_DT       {get;set;}
		public string SHLS_SHIP_TYPE     {get;set;}
		public string SHLS_SHIP_AMT      {get;set;}
		public string SHLS_SHIP_PCNT     {get;set;}
		public string SHLS_COMMENT       {get;set;}
		public string DASY_KY            {get;set;}
		public string DASY_DTM           {get;set;}
		public string DASY_TYPE          {get;set;}
		public string DASY_OPRT          {get;set;}
		public string DASY_ID            {get;set;}
		public string DASY_OPRT_USER { get; set; }
    }
}
