using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NetWorks.Core.Framework;

namespace NetWorks.SampleApp
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(configuration, webHostEnvironment)
        {
        }
    }
}