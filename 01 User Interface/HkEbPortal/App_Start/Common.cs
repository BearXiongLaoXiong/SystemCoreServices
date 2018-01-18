using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;
using Org.BouncyCastle.Asn1.Ocsp;

namespace HkEbPortal.App_Start
{
    public class Common
    {
        private static readonly string[] UrlRef = ConfigurationManager.AppSettings["RequestUrlReferrerAuthorize"]?.Split(',');
        private static readonly string Page404 = ConfigurationManager.AppSettings["Page404"]??"";
        private readonly ICommonBl _commonBl = new CommonBl();
        /// <summary>
        /// true : 开放个人保单,false :关闭个人保单
        /// </summary>
        /// <param name="memeKy"></param>
        /// <returns></returns>
        public bool IsOpenEnrollment(string memeKy)
        {
            int.TryParse(memeKy, out int meKy);
            var entity = new SPEH_EBEB_IS_OPEN
            {
                pMEME_KY = meKy
            };
            _commonBl.Execute(entity);
            return entity.pIsOpen == 0;
        }

        public static void RequestUrlReferrerCheck(Uri url, Uri referrer , HttpApplication app)
        {
            if (UrlRef.Contains(url.AbsolutePath.ToLower())) return;
            if (referrer == null || url.Host != referrer.Host)
            {
                Debug.WriteLine("Authority  不相同,直接reutrn ");
                app.Response.Redirect(Page404);
            }
            else Debug.WriteLine("Authority 相同");
        }
    }
}