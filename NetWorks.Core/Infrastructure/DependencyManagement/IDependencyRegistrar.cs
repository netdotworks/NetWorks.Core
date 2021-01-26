using Autofac;
using Microsoft.Extensions.Configuration;

namespace NetWorks.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, IConfiguration configuration);

        int Order { get; }
    }
}