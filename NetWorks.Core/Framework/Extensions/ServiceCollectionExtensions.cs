using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetWorks.Core.Infrastructure;
using System.Net;

namespace NetWorks.Core.Framework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IEngine ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            services.AddHttpContextAccessor();

            var engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration);

            return engine;
        }
    }
}