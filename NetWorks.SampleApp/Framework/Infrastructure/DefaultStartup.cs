using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetWorks.Core.DataAccess;
using NetWorks.Core.Infrastructure;

namespace NetWorks.SampleApp.Framework.Infrastructure
{
    public class DefaultStartup : BaseAppStartup
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DefaultStartup).Assembly.GetName().Name;

            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(assembly)));

            services.AddControllers();
        }

        public override void Configure(IApplicationBuilder application)
        {
            application.UseStaticFiles();
            application.UseRouting();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}