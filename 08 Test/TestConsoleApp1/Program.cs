using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Framework.Aop;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicRepository;
using Ftp.Entities;


namespace TestConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {







            var list = Enumerable.Range(0, 10).ToDictionary(i => i, j => 0);
            var taskList = new List<Task>();
            ICommonBl _commonBl = new CommonBl();
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    TestBll tbs = new TestBll();
                    tbs.ConnectionString = i.ToString();
                    _commonBl.Execute(tbs);

                    //TypeDescriptor.AddAttributes(typeof(TestBll), new DatabaseConnectionAttribute(f.ToString()));
                    //var con = int.Parse(TypeDescriptor.GetAttributes(typeof(TestBll)).OfType<DatabaseConnectionAttribute>().FirstOrDefault()?.ConnectionString ?? "0");
                    if (i % 1000 == 0) Console.WriteLine(i);
                    //list[f]++;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            Console.ReadLine();
            taskList.AddRange(list.Keys.Select(f => Task.Factory.StartNew(x =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    try
                    {
                        //TypeDescriptor.AddAttributes(typeof(TestBll), new DatabaseConnectionAttribute(f.ToString()));
                        //var con = int.Parse(TypeDescriptor.GetAttributes(typeof(TestBll)).OfType<DatabaseConnectionAttribute>().FirstOrDefault()?.ConnectionString ?? "0");
                        if (i % 1000 == 0) Console.WriteLine(i);
                        list[f]++;
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }, 1)));
            Task.WaitAll(taskList.ToArray());
            foreach (var item in list)
                Console.WriteLine($"{item.Key}:\t{item.Value}");

            
            TestBll tb = new TestBll();

            Console.ReadLine();

            BearTest res = new BearTest();
            //res.pTKTK_KY = new List<IMIM_PATH> { new IMIM_PATH { IMIM_PATH_NAME="gagag"}, new IMIM_PATH { IMIM_PATH_NAME = "asdfafda" } }.ToSqlDataRecord().ToList();

            //var list = new List<IMIM_STTS_UPDATE> { new IMIM_STTS_UPDATE() { IMIM_KY = 19491533 } };
            //list.ToSqlDataRecord().ToList();

            ICommonBl _commonBl1 = new CommonBl();
            _commonBl1.Execute(res);

            //_01OcrRpcTest test = new _01OcrRpcTest();
            //test.Send();
            ////var v1 = Container.Resolve<ITestBll>();

            ////var v2 = Container.Resolve<ITestBll>();
            //var config = new ConfigurationBuilder();
            //// config.AddJsonFile comes from Microsoft.Extensions.Configuration.Json
            //// config.AddXmlFile comes from Microsoft.Extensions.Configuration.Xml
            //config.AddJsonFile("autofac.json");

            //// Register the ConfigurationModule with Autofac.
            //var module = new ConfigurationModule(config.Build());
            //var builder = new ContainerBuilder();
            //builder.RegisterModule(module);
            //var _container = builder.Build();
            //var test = _container.Resolve<ITestBll>();
            //test.GetName();
            ////var v3 = Container.Resolve<ITestBll>();
            ////Container.testBll.GetName();
            ////v3.GetName();
            Console.ReadLine();
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
    [DatabaseConnection(ConnectionEnum.CustomizeConnectionString)]
    public class TestBll : ITestBll, ICustomizeConnectionString
    {
        public int hash = 0;
        public TestBll()
        {
            hash = GetHashCode();
            Console.WriteLine("创建实例" + hash);
        }
        public string GetName()
        {
            Console.WriteLine("注册成功");
            return "有熊";
        }

        public string ConnectionString { get; set; }
    }


    //public class Container
    //{
    //    private static IContainer _container;
    //    public static T Resolve<T>()
    //    {
    //        if (_container == null)
    //            InitializeComponent();
    //        return _container.Resolve<T>();
    //    }

    //    public static void InitializeComponent()
    //    {
    //        var builder = new ContainerBuilder();
    //        //builder.Register(c => new TestBll()).As<ITestBll>();
    //        //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
    //        _container = builder.Build();
    //    }
    //}
}


namespace MytestDi
{
    public interface IMyTest
    {
        void go();
    }

    public class MyTest : IMyTest
    {
        public void go()
        {
            Console.WriteLine("gogogo");
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