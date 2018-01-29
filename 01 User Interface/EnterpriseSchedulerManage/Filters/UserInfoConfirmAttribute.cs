using EnterpriseSchedulerManage.Models.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EnterpriseSchedulerManage.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UserInfoConfirmAttribute : FilterAttribute, IAuthorizationFilter
    {
        public UserInfoConfirmAttribute()
        {
            String authUrl = ConfigurationManager.AppSettings["UserInfoConfirm"];

            _authUrl = String.IsNullOrEmpty(authUrl) ? "../Home/Index" : authUrl;
        }

        /// <summary>
        /// 获取或者设置一个值，该值表示登录地址
        /// 如果web.config中末定义AuthUrl的值，则默认为：/waste/user/login
        /// </summary>
        private String _authUrl;

        public String AuthUrl
        {
            get { return _authUrl.Trim(); }
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
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                UserInfo entity = ((UserInfo)filterContext.HttpContext.Session[FormsAuthentication.FormsCookieName]);
                //无访问此Action权限
                if (entity == null || string.IsNullOrWhiteSpace(entity?.USUS_LOGIN) || string.IsNullOrWhiteSpace(entity.USUS_NAME))
                {
                    filterContext.Result = new RedirectResult(_authUrl);
                }

            }
        }
    }
}