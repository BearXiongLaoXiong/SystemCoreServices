using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp1
{
    public class _02AbbributeParameterTest
    {
        public void TestMethod([Parameter]string text)
        {
            
        }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = true)]
    public class ParameterAttribute : Attribute
    {
        public ParameterAttribute()
        {

        }
    }
}
