using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCore.Entities.SystemSetting
{
    public class TargetSource
    {
        public TargetSource(SPEH_DASY_SYNC_CODE_LIST_RESULT row)
        {
            Ky = row.Id;
            Name = row.Name;
            Type = row.Comment;
        }

        public string Ky { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
