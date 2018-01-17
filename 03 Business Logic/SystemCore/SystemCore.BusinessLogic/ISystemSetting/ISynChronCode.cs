using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCore.Entities.SystemSetting;

namespace SystemCore.BusinessLogic.ISystemSetting
{
    public interface ISynChronCode
    {
        List<SPEH_SYSV_VALUE_LIST_RESULT> GetSynChronContent();

        List<SPEH_DASY_SYNC_CODE_LIST_RESULT> GetSynChronTarget();

        List<SPEH_HPHP_HOSPITAL_INFO_LIST_SYNC_RESULT> GetHPHPCodeByCondition(string synchronContent, string source, string codeValue, string startDate, string endDate);
    }
}
