using Autofac;
using Microsoft.Extensions.Configuration;
using NetWorks.Core.Infrastructure;
using NetWorks.Core.Infrastructure.DependencyManagement;

namespace NetWorks.Core.Logic.DependencyInjections
{
    public class DatabaseLogicDependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IConfiguration configuration)
        {
            builder.RegisterGeneric(typeof(DatabaseLogic<>)).As(typeof(IDatabaseLogic<>)).InstancePerLifetimeScope();
        }
    }
}