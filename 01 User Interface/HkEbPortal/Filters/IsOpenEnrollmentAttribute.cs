using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HkEbPortal.App_Start;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class IsOpenEnrollmentAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IsOpenEnrollmentAttribute()
        {
            String authUrl = ConfigurationManager.AppSettings["HomeIndex"];

            _authUrl = String.IsNullOrEmpty(authUrl) ? "../eflexi/Home/Index" : authUrl;
        }

        /// <inheritdoc />
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="isOpened">表示没有登录跳转的登录地址</param>
        public IsOpenEnrollmentAttribute(bool isOpened) : this()
        {
            _isOpened = isOpened;
        }

        /// <summary>
        /// 获取或者设置一个值，该值表示登录地址
        /// 如果web.config中末定义AuthUrl的值，则默认为：/waste/user/login
        /// </summary>
        private String _authUrl;

        public String AuthUrl
        {
            get => _authUrl.Trim();
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException($"用于验证用户登录信息的登录地址不能为空！");
                _authUrl = value.Trim();
            }
        }

        private bool _isOpened = false;


        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Debug.WriteLine("is open");
            if (filterContext.HttpContext == null)
                throw new Exception("此特性只适合于Web应用程序使用！");
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                //无访问此Action权限
                if (new Common().IsOpenEnrollment(((UserInfo)filterContext.HttpContext.Session[FormsAuthentication.FormsCookieName])?.USUS_KY) == _isOpened)
                {
                    filterContext.Result = new RedirectResult(_authUrl);
                }

            }
        }
    }
}