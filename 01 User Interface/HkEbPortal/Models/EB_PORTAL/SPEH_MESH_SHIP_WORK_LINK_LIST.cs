using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HkEbPortal.Models.EB_PORTAL
{
    /// <summary>
    /// 被保险人地区
    /// </summary>
    public class SPEH_MESH_SHIP_WORK_LINK_LIST
    {
        public string pMEME_KY { get; set; }
        public string pEHUSER { get; set; }
        public string lang { get; set; }
    }

    public class SPEH_MESH_SHIP_WORK_LINK_LIST_RESULT
    {
        public string MESH_KY { get; set; }
        public string MEME_KY { get; set; }
        public string SYSV_MESH_TYPE { get; set; }
        public string SYSV_MESH_TYPE_DESC { get; set; }
        public string MESH_EFF_DT { get; set; }
        public string MESH_END_DT { get; set; }
        public string SHSH_KY { get; set; }
        public string SHSH_NAME { get; set; }
        public string ALT_SHSH_KY { get; set; }
        public string MESH_COMMENT { get; set; }
    }
}