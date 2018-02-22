using System;
using System.Data;
using System.Framework.Aop;

namespace HkEbPortal.Models.EB_PORTAL
{
    public class SPEH_FMFM_LOGIN
    {
        public string pPOLICYNO { get; set; }
        public string pUSUS_ID { get; set; }
        public string pUSUS_PSWD { get; set; }

        [SqlParameter(555, direction: ParameterDirection.Output)]
        public string pRTN_MSG { get; set; }

        [SqlParameter(direction: ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }
    }

    public class UserInfo
    {
        public string USUS_ID { get; set; }
        public string USUS_KEY_TYPE { get; set; }
        public string USUS_KY { get; set; }
        public string USUS_EMAIL { get; set; }
        public string USUS_EMAIL_ISACTIVE { get; set; }
        public string USUS_FIRST_ISACTIVE { get; set; }
        public string USUS_INFO_IS_CONFIRM { get; set; }
        public string PFPF_ID { get; set; }
        public string ENTT_CLIENT_CD { get; set; }
        public string ENTT_KY { get; set; }
        public string ENTT_NAME { get; set; }
        public string GPPN_KY { get; set; }
        public string GPPN_NAME { get; set; }
        public string GPGP_KY { get; set; }
        public string GPGP_NAME { get; set; }
        public string FMFM_KY { get; set; }
        public string NAME { get; set; }
        public string INTIAL_AMT { get; set; }
        public string FMFM_CUR_AMT { get; set; }
        public string GCGC_KY { get; set; }
        public string GCGC_DESC { get; set; }
        public string TAX_CUR_AMT { get; set; }
        public string MEME_KY { get; set; }
        public string MEME_NAME { get; set; }
        public string MEME_CERT_ID_NUM { get; set; }
        public string ENTT_LANG_ID { get; set; }
        public string ENTT_DPT_ID { get; set; }
        public UserType UserType
        {
            get
            {
                switch (ENTT_DPT_ID)
                {
                    case "Observer": return UserType.Observer;
                    case "CS": return UserType.Cs;
                    case "": return UserType.Member;
                    default: return UserType.Bk;
                }
            }
        }

        /// <summary>
        /// 对 bk  cs 隐藏菜单
        /// </summary>
        public bool IsDisabledByBk => UserType == UserType.Member || UserType == UserType.Observer;

        /// <summary>
        /// 除了Member其他不具写权限
        /// </summary>
        public bool IsMember =>  UserType == UserType.Member;
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    [Flags]
    public enum UserType
    {
        /// <summary>
        /// 匿名用户
        /// </summary>
        None = 0,
        /// <summary>
        /// 被保险人
        /// </summary>
        Member = 1,
        /// <summary>
        /// 保险公司用户
        /// </summary>
        Cs = 2,
        /// <summary>
        /// 经纪人
        /// </summary>
        Bk = 4,
        /// <summary>
        /// 只有查看权限,cs、bk选择用户后转化成此权限
        /// </summary>
        Observer = 8,
    }

}