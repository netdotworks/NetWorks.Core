using Microsoft.AspNetCore.Builder;
using NetWorks.Core.Infrastructure;

namespace NetWorks.Core.Framework.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }
    }
}