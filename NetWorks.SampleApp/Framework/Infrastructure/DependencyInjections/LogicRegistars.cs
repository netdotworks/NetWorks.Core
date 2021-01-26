using Autofac;
using Microsoft.Extensions.Configuration;
using NetWorks.Core.Infrastructure;
using NetWorks.Core.Infrastructure.DependencyManagement;
using NetWorks.SampleApp.Framework.Logic;

namespace NetWorks.SampleApp.Framework.Infrastructure.DependencyInjections
{
    public class LogicRegistars : IDependencyRegistrar
    {
        public int Order => 100;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IConfiguration configuration)
        {
            builder.RegisterType<ProductLogic>().As<IProductLogic>().InstancePerLifetimeScope();
        }
    }
}