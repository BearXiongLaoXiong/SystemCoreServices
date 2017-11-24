using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HkEbPortal.Controllers;

namespace HkEbPortal.Filters
{
    public class ParameterValidato
    {
        public static A GetParameterValidator<C, A>(string parameterName, string name, Type[] types)
        {
            return typeof(C).GetMethod(name, types).GetParameters().FirstOrDefault(x => x.Name == parameterName).GetCustomAttributes(typeof(A), true).Cast<A>().FirstOrDefault();

        }
    }
}