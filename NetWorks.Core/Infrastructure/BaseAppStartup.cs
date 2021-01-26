using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetWorks.Core.Infrastructure
{
    public abstract class BaseAppStartup : IAppStartup
    {
        public virtual int Order => 0;

        public virtual void Configure(IApplicationBuilder application)
        {
        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}