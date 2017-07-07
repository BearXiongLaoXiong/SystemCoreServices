using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class SPEH_DCDC_INFO_LIST_SYNC
    {
        public string pDASY_TYPE { get; set; }

        public string pCODE { get; set; }

        public string pDASY_START_DATE { get; set; }

        public string pDASY_END_DATE { get; set; }
    }

    public class SPEH_DCDC_INFO_LIST_SYNC_RESULT
    {
        public string DCDC_ID        {get;set;}
		public string DCDC_TYPE      {get;set;}
		public string DCDC_DESC      {get;set;}
		public string DCDC_DESC_ENG  {get;set;}
		public string DCDC_ID2       {get;set;}
		public string DCDC_DESC2     {get;set;}
		public string DCDC_DESC_ENG2 {get;set;}
		public string DCDC_ID3       {get;set;}
		public string DCDC_DESC3     {get;set;}
		public string DCDC_DESC_ENG3 {get;set;}
		public string STST_ID        {get;set;}
		public string STST_ID2       {get;set;}
		public string STST_DESC      {get;set;}
		public string STST_DESC_ENG  {get;set;}
		public string DASY_KY        {get;set;}
		public string DASY_DTM       {get;set;}
		public string DASY_TYPE      {get;set;}
		public string DASY_OPRT      {get;set;}
		public string DASY_ID        {get;set;}
		public string DASY_OPRT_USER { get; set; }
    }
}
