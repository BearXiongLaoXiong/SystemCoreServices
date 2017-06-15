using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.Aop
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlParameterAttribute : System.Attribute
    {
        //public SqlDbType SqlDbType { get; set; }

        public int Size { get; set; }

        public ParameterDirection Direction { get; set; }

        public bool IsSizeDefined => Size != 0;

        public string TypeName { get; set; }

        public SqlParameterAttribute(int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            //SqlDbType = sqlDbType;
            Size = size;
            Direction = direction;
        }
    }
}
