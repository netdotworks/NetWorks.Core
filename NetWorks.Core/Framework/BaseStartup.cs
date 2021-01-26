using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetWorks.Core.Framework.Extensions;
using NetWorks.Core.Infrastructure;

namespace NetWorks.Core.Framework
{
    public class BaseStartup
    {
        private IEngine _engine;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BaseStartup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            _engine = services.ConfigureApplicationServices(_configuration);
        }

        public virtual void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            _engine.RegisterDependencies(containerBuilder, _configuration);
        }

        public virtual void Configure(IApplicationBuilder application)
        {
            application.ConfigureRequestPipeline();
        }
    }
}