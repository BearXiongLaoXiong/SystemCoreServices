using BusinessLogicRepository;
using EnterpriseSchedulerManage.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EnterpriseSchedulerManage.Controllers
{
    public class BaseController : Controller
    {
        public readonly ICommonBl CommonBl = new CommonBl();

        public UserInfo UserInfo
        {
            get { return Session[FormsAuthentication.FormsCookieName] as UserInfo; }
            set { UserInfo = value; }
        }
    }
}