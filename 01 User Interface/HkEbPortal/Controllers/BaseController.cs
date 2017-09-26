using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Controllers
{
    public class BaseController : Controller
    {
        public readonly ICommonBl CommonBl = new CommonBl();

        public UserInfo UserInfo
        {
            get => Session[FormsAuthentication.FormsCookieName] as UserInfo;
            set => UserInfo = value;
        }
    }
}