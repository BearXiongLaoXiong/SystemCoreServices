using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_PLPL_DOC_LIST
    {
        public int pUSUS_KY { get; set; }
        public int pID_KY { get; set; }
        public string lang { get; set; }
    }

    public class SPEH_PLPL_DOC_LIST_RESULT
    {
        public string PLPL_KY { get; set; }
        public string DOC_PATH { get; set; }
        public string TITLE { get; set; }
        public string SIZE { get; set; }
        public string IS_ACTIVE { get; set; }
        private string _createDate { get; set; }
        public string CREATE_DATE
        {
            get => string.IsNullOrWhiteSpace(_createDate) ? "" : Convert.ToDateTime(_createDate).ToString("dd/MM/yyyy");
            set => _createDate = value;
        }
        public string Extension => DOC_PATH.Length > 0 ? System.IO.Path.GetExtension(DOC_PATH).Replace(".", "").ToUpper() : "*";
    }
}