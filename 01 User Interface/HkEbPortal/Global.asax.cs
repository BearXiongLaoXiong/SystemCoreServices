using System;
using System.Diagnostics;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using HkEbPortal.App_Start;

namespace HkEbPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            if (sender is HttpApplication app)
            {
                //移除Server
                app.Context.Response.Headers.Remove("Server");
                //修改Server的值
                //app.Context.Response.Headers.Set("Server", "MyPreciousServer");
                app.Context.Response.Headers.Remove("X-AspNet-Version");
                app.Context.Response.Headers.Remove("X-AspNetMvc-Version");
            }
        }


        protected void Session_Start(Object sender, EventArgs e)
        {
            //Response.Headers.Add("Set-Cookie", $"ASP.NET_SessionId={Response.Cookies["ASP.NET_SessionId"]?.Value}; expires={ DateTime.Now.AddHours(1)}; path=/; Secure; HttpOnly");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("-----------------------------URL      = " + Request.Url);
            Debug.WriteLine("-----------------------------referrer = "+Request.UrlReferrer?.AbsoluteUri);
            Common.RequestUrlReferrerCheck(Request.Url, Request.UrlReferrer, sender as HttpApplication);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //var cookie = Response.Cookies[FormsAuthentication.FormsCookieName];
            //if (cookie != null)
            //    Response.AddHeader("Set-Cookie", $"{FormsAuthentication.FormsCookieName}={cookie.Value}; expires={ DateTime.Now.AddHours(1)}; path=/;  HttpOnly");
            if (Response.Cookies.Count > 0)
            {
                foreach (string s in Response.Cookies.AllKeys)
                {
                    //Debug.WriteLine("-----------------------------------------------" + s + "  --------  " + Response.Cookies[s].Value);
                    if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid")
                    {
                        if (Request.IsSecureConnection)
                            Response.Cookies[s].Secure = true;
                        Response.Cookies[s].HttpOnly = true;

                    }
                }
            }

        }

    }
}
