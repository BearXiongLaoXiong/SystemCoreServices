using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using Microsoft.Extensions.Configuration;

namespace System.Framework.Autofac
{
    public class Containers
    {
        private static IContainer _container;

        public static T Resolve<T>()
        {
            if (_container == null) InitializeComponent();
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(string serviceName)
        {
            if (_container == null) InitializeComponent();
            return _container.ResolveNamed<T>(serviceName);
        }

        public static T Resolve<T>(params Parameter[] parameters)
        {
            if (_container == null)
                InitializeComponent();
            return _container.Resolve<T>(parameters);
        }

        public static T Resolve<T>(string serviceName, params Parameter[] parameters)
        {
            if (_container == null) InitializeComponent();
            return _container.ResolveKeyed<T>(serviceName, parameters);
        }

        private static void InitializeComponent()
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile("Configs/autofac.json");

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
            _container = builder.Build();
        }
    }
}
