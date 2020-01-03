using Microsoft.Extensions.DependencyInjection;
using ViajaNet.JobApplication.Application;

namespace ViajaNet.JobApplication.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddViajaNetApplication(this IServiceCollection services)
        {
            services.AddViajaNetProviders();

            return services.AddTransient<IAnalyticsAppService, AnalyticsAppService>();
        }
    }
}
