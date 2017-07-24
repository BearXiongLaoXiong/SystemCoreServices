using System;
using System.Collections.Generic;

using Autofac;
using Autofac.Configuration;

namespace TestConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            var v1 = Container.Resolve<ITestBll>();

            var v2 = Container.Resolve<ITestBll>();


            var v3 = Container.Resolve<ITestBll>();
            //Container.testBll.GetName();
            v3.GetName();
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
        public int hash = 0;
        public TestBll()
        {
            hash = GetHashCode();
            Console.WriteLine("创建实例" + hash);
        }
        public string GetName()
        {
            return "有熊";
        }
    }


    public class Container
    {
        private static IContainer _container;
        public static T Resolve<T>()
        {
            if (_container == null)
                InitializeComponent();
            return _container.Resolve<T>();
        }

        public static void InitializeComponent()
        {
            var builder = new ContainerBuilder();
            //builder.Register(c => new TestBll()).As<ITestBll>();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            _container = builder.Build();
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