using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class SPEH_DADA_DIAGNOSIS_INFO_LIST_SYNC
    {
        public string pDASY_TYPE { get; set; }

        public string pCODE { get; set; }

        public string pDASY_START_DATE { get; set; }

        public string pDASY_END_DATE { get; set; }

    }

    public class SPEH_DADA_DIAGNOSIS_INFO_LIST_SYNC_RESULT
    {
          public string SEQ { get; set; }
          public string DADA_ID          {get;set;}
          public string DADA_DESC        {get;set;}
          public string DADA_DESC_ENG    {get;set;}
          public string DADA_ID_REL      {get;set;}
          public string SYSV_DADA_TYPE   {get;set;}
          public string DADA_SEX_IND     {get;set;}
          public string DADA_AGE_FROM    {get;set;}
          public string DADA_AGE_TO      {get;set;}
          public string DADA_CVT_LEVEL   {get;set;}
          public string DADA_MORB_RATE   {get;set;}
          public string DADA_IP_COST     {get;set;}
          public string DADA_OP_COST     {get;set;}
          public string DADA_ANNUAL_VISIT{get;set;}
          public string DADA_EXEX_ID     {get;set;}
          public string DADA_ACTION      {get;set;}
          public string DADA_NAME_FST    {get;set;}
          public string DADA_NAME_FUL    {get;set;}
	      public string DASY_KY          {get;set;}
	      public string DASY_DTM         {get;set;}
	      public string DASY_TYPE        {get;set;}
	      public string DASY_OPRT        {get;set;}
	      public string DASY_ID          {get;set;}
	      public string DASY_OPRT_USER { get; set; }
    }
}
