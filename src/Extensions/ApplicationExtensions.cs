using Microsoft.Extensions.DependencyInjection;
using ViajaNet.JobApplication.Application.Service;

namespace ViajaNet.JobApplication.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddViajaNetApplication(this IServiceCollection services)
        {
            services.AddViajaNetProviders()
                .AddViajaNetRepositories();

            return services.AddTransient<IAnalyticsAppService, AnalyticsAppService>();
        }
    }
}
