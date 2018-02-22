using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// 表示需要用户登录才可以使用的特性
    /// 如果不需要处理用户登录，则请指定AllowAnonymousAttribute属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public UserType UserType = UserType.Member | UserType.Observer;

        /// <inheritdoc />
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AuthorizationAttribute()
        {
            String authUrl = ConfigurationManager.AppSettings["UserLogin"];

            _authUrl = String.IsNullOrEmpty(authUrl) ? "../eflexi/User/Login" : authUrl;
        }

        /// <inheritdoc />
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="authUrl">表示没有登录跳转的登录地址</param>
        public AuthorizationAttribute(String authUrl) : this()
        {
            _authUrl = authUrl;
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

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null)
                throw new Exception("此特性只适合于Web应用程序使用！");
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                    typeof(AllowAnonymousAttribute), true)) return;
            if (filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                filterContext.Result = new RedirectResult(_authUrl);
            else if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                if (filterContext.HttpContext.Session[FormsAuthentication.FormsCookieName] == null)
                {
                    //无访问此Action权限
                    filterContext.Result = new RedirectResult(_authUrl);
                }
                else
                {
                    if (HasFlag(filterContext.HttpContext.Session[FormsAuthentication.FormsCookieName])) return;
                    //无访问此Action权限
                    filterContext.Result = new RedirectResult(_authUrl);
                }
            }
        }

        private bool HasFlag(object session)
        {
            var userInfo = session as UserInfo;
            return UserType != UserType.None && UserType.HasFlag(userInfo.UserType);
        }
    }
}