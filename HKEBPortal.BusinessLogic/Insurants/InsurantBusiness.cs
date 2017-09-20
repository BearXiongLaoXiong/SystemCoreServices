using BusinessLogicRepository;
using HKEBPortal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKEBPortal.BusinessLogic
{
    public class InsurantBusiness
    {
        private readonly ICommonBl _commonBl = new CommonBl();


        public List<SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT> GetMemberInfoList()
        {
            SPEH_FMDT_FAMILY_DETL_LIST_WEB entity = new SPEH_FMDT_FAMILY_DETL_LIST_WEB()
            {
                pEHUSER =""
            };

            var list = _commonBl.QuerySingle<SPEH_FMDT_FAMILY_DETL_LIST_WEB, SPEH_FMDT_FAMILY_DETL_LIST_WEB_RESULT>(entity);

            return list;
        }
    }
}
