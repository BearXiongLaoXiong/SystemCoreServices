using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Security;
using System.Web.Services.Description;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Filters
{
    /// <summary>
    /// PolicyNo授权
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = true)]
    public class PolicyNoAttribute : ValidationAttribute
    {
        private readonly ICommonBl _commonBl = new CommonBl();
        public bool IsValid(object value, params string[] param)
        {
            if (value == null) return false;
            var entity = new SPEH_PLPL_AUTHORIZE { pPLPL_ID = $"{param[0].Split('-')[0]}-0", pPLPL_KY = value.ToString() };
            _commonBl.Execute(entity);
            return entity.ReturnValue == 1;
        }
    }


}

