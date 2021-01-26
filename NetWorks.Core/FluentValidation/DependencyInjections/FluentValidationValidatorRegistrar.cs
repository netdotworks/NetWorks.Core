using Autofac;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using NetWorks.Core.Infrastructure;
using NetWorks.Core.Infrastructure.DependencyManagement;
using System.Linq;

namespace NetWorks.Core.FluentValidation.DependencyInjections
{
    public class FluentValidationValidatorRegistrar : IDependencyRegistrar
    {
        public int Order => 9000;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IConfiguration configuration)
        {
            builder.RegisterType<HarFluentValidationValidatorFactory>().As<IValidatorFactory>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeFinder.GetAssemblies().ToArray()).AsClosedTypesOf(typeof(IValidator<>)).InstancePerLifetimeScope();
        }
    }
}