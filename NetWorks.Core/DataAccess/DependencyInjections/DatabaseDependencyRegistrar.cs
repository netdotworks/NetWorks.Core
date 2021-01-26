using Autofac;
using Microsoft.Extensions.Configuration;
using NetWorks.Core.DataAccess.Repositories;
using NetWorks.Core.Infrastructure;
using NetWorks.Core.Infrastructure.DependencyManagement;

namespace NetWorks.Core.DataAccess.DependencyInjections
{
    public class DatabaseDependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IConfiguration configuration)
        {
            builder.RegisterGeneric(typeof(BaseRepo<>)).As(typeof(IRepo<>)).InstancePerLifetimeScope();
        }
    }
}