using System;
using System.Collections.Generic;
using System.Framework.Aop;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ftp.BusinessLogic._Base;
using Ftp.Entities;

namespace TestConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] number = {1, 2, 3};
            int[] n1 = new int[6];
            int [,] n2 = new int[2,3];
            var a = new[] {1, 2, 3};
            // var result = Getvalues(x => x.ConnectionString);
        }

        //static string Getvalues(Expression<Func<DatabaseConnectionAttribute, string> attributeValueAction> exp)
        //{
        //    return attributeValueAction.ToString();
        //}
    }
}









//var entity = new SPIN_FLFL_FILE_LOG_INFO_INSERT
//{
//pFLFL_STS = "2",
//pFILE_NAME = "testname",
//pFLFL_URL = "testurl",
//pFLFL_TYPE = "testtype",
//pFLFL_USUS_ID = "admin"

//};
//ICommonBl _commonBl = new CommonBl();
//_commonBl.Execute(entity);