using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.Filters
{
    /// <summary>
    /// Faimly MemberId授权
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = true)]
    public class FaimlyAttribute : ValidationAttribute
    {
        private readonly ICommonBl _commonBl = new CommonBl();
        public bool IsValid(object value, params string[] param)
        {
            if (value == null) return false;
            var entity = new SPEH_FAIMLY_AUTHORIZE { pUSUS_KY = int.Parse(param[0]),pMEME_KY= int.Parse(value.ToString()) };
            _commonBl.Execute(entity);
            return entity.ReturnValue == 1;
        }
    }
}