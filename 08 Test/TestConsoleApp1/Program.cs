using System;
using System.Collections.Generic;
using System.Framework.Aop;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Ftp.Entities;

namespace TestConsoleApp1
{
    class Program
    {
        public class Entity
        {
            public string name;
            public string weight;
        }
        static void Main(string[] args)
        {
            List<Entity> ent = new List<Entity>()
            {
                new Entity() {name = "一", weight = "1"},
                new Entity() {name = "二", weight = "2"}
            };


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

    public interface ITestBll
    {
        string GetName();
    }

    public class TestBll : ITestBll
    {
        public string GetName()
        {
            return "有熊";
        }
    }

    public class Container
    {
        public static IContainer container = null;

        public static T Resolve<T>()
        {
            if (container == null)
            {
                
            }
            return container.Resolve<T>();
        }

        public static void Initialise()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestBll>().As<ITestBll>();
        }

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