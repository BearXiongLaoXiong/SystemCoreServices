using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class SPEH_SPSP_DIAGNOSIS_INFO_LIST_SYNC
    {
        public string pDASY_TYPE { get; set; }

        public string pCODE { get; set; }

        public string pDASY_START_DATE { get; set; }

        public string pDASY_END_DATE { get; set; }
    }

    public class SPEH_SPSP_DIAGNOSIS_INFO_LIST_SYNC_RESULT
    {
        public string SEQ { get; set; }
        public string SPSP_ID             {get;set;}
        public string SYSV_SPSP_TYPE      {get;set;}
        public string SYSV_SPSP_LEVEL     {get;set;}
        public string DODO_KY             {get;set;}
        public string SPSP_DESC           {get;set;}
        public string SPSP_DESC_ENG       {get;set;}
        public string REF_ID              {get;set;}
        public string REF_ID2             {get;set;}
        public string REF_ID3             {get;set;}
        public string COMMENT             {get;set;}
        public string SPSP_NAME_FST       {get;set;}
        public string SPSP_NAME_FUL       {get;set;}
        public string DASY_KY             {get;set;}
        public string DASY_DTM            {get;set;}
        public string DASY_TYPE           {get;set;}
        public string DASY_OPRT           {get;set;}
        public string DASY_ID             {get;set;}
        public string DASY_OPRT_USER { get; set; }
    }
}
