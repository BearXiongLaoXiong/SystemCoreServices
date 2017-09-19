using System.Configuration;
using Autofac;
using Autofac.Configuration;
using Autofac.Core;

namespace System.Framework.Autofac
{
    public class Containers
    {
        private static IContainer _container;
        public static T Resolve<T>()
        {
            if (_container == null)
                InitializeComponent();
            return _container.Resolve<T>();
        }
        public static T Resolve<T>(params Parameter[] parameters)
        {
            if (_container == null)
                InitializeComponent();
            return _container.Resolve<T>(parameters);
        }

        private static void InitializeComponent()
        {
            var builder = new ContainerBuilder();
            //builder.Register(c => new TestBll()).As<ITestBll>();
            //var configurationFile = ConfigurationManager.AppSettings["AutofacConfigurationFile"];
            //if (string.IsNullOrEmpty(configurationFile)) throw new ArgumentNullException($"not found AutofacConfigurationFile!");
            builder.RegisterModule(new ConfigurationSettingsReader("autofac")); 
            //builder.RegisterModule(new ConfigurationSettingsReader("BusinessLogic", configurationFile));
            _container = builder.Build();
        }
    }
}
