using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetWorks.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetWorks.Core.Infrastructure
{
    public class AppEngine : IEngine
    {
        private ITypeFinder _typeFinder;

        private IServiceProvider _serviceProvider { get; set; }

        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        protected IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider == null)
                return null;
            var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        public void RegisterDependencies(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

            containerBuilder.RegisterInstance(_typeFinder).As<ITypeFinder>().SingleInstance();

            var dependencyRegistrars = _typeFinder.FindClassesOfType<IDependencyRegistrar>();

            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, _typeFinder, configuration);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //check for assembly already loaded
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            //get assembly from TypeFinder
            var tf = Resolve<ITypeFinder>();
            if (tf == null)
                return null;
            assembly = tf.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return assembly;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            _typeFinder = new AppDomainTypeFinder();
            var startupConfigurations = _typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startupConfigurations
                .Select(startup => (IAppStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            _serviceProvider = application.ApplicationServices;

            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startupConfigurations
                .Select(startup => (IAppStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
                instance.Configure(application);
        }

        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            var sp = GetServiceProvider();
            if (sp == null)
                return null;
            return sp.GetService(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        public object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new Exception("Unknown dependency");
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new Exception("No constructor was found that had all the dependencies satisfied.", innerException);
        }
    }
}