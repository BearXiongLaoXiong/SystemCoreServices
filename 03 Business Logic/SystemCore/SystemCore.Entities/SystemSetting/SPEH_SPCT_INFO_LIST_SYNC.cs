using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class SPEH_SPCT_INFO_LIST_SYNC
    {
        public string pDASY_TYPE { get; set; }

        public string pCODE { get; set; }

        public string pDASY_START_DATE { get; set; }

        public string pDASY_END_DATE { get; set; }
    }

    public class SPEH_SPCT_INFO_LIST_SYNC_RESULT
    {
        public string SEQ { get; set; }
        public string SPCT_ID          {get;set;}
          public string SYSV_SPCT_TYPE   {get;set;}
          public string SPCT_DESC        {get;set;}
          public string SPCT_DESC_ENG    {get;set;}
          public string OTC_IND          {get;set;}
          public string CAT_ID           {get;set;}
          public string CAT_ID2          {get;set;}
          public string CAT_ID3          {get;set;}
          public string COMMENT          {get;set;}
          public string SPCT_NAME_FST    {get;set;}
          public string SPCT_NAME_FUL    {get;set;}
	      public string DASY_KY          {get;set;}
	      public string DASY_DTM         {get;set;}
	      public string DASY_TYPE        {get;set;}
	      public string DASY_OPRT        {get;set;}
	      public string DASY_ID          {get;set;}
	      public string DASY_OPRT_USER { get; set; }
    }
}
