using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_INDEX_CAROUSEL_IMAGE_INFO_LIST
    {
        public string pUSUS_ID { get; set; }
    }


    public class SPEH_INDEX_CAROUSEL_IMAGE_INFO_LIST_RESULT
    {
        public int GPGP_KY { get; set; }
        public string IMAGE_PATH { get; set; }
        public string FILE_NAME => $@"../Upload/CarouselImages/{GPGP_KY}/{IMAGE_PATH}";
        public string TITLE { get; set; }
        public string TEXT1 { get; set; }
        public string TEXT2 { get; set; }
        public string TEXT3 { get; set; }
        public string HREF { get; set; }
        public int ORDER_ID { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string USUS_ID { get; set; }
    }
}