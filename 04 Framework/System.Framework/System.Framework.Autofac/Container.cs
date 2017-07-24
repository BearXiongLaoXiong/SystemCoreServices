using System.Configuration;
using Autofac;
using Autofac.Configuration;

namespace System.Framework.Autofac
{
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
            var configurationFile = ConfigurationManager.AppSettings["AutofacConfigurationFile"];
            if (string.IsNullOrEmpty(configurationFile)) throw new ArgumentNullException($"not found AutofacConfigurationFile!");
            builder.RegisterModule(new ConfigurationSettingsReader("autofac",configurationFile));
            _container = builder.Build();
        }
    }
}
