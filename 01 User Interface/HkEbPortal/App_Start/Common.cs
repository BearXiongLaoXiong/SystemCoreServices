using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicRepository;
using HkEbPortal.Models.EB_PORTAL;

namespace HkEbPortal.App_Start
{
    public class Common
    {
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
    }
}