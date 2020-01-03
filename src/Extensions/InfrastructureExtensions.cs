using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViajaNet.JobApplication.Infrastructure.Queue;

namespace ViajaNet.JobApplication.Extensions
{
    public static class InfrastructureExtensions
    {
        private const string QueueConfigurationSection = "PubSub:RabbitMq";

        public static IServiceCollection AddViajaNetProviders(this IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", optional: true);

            var configuration = configBuilder.Build();

            return services.AddOptions()
                .Configure<QueueConfiguration>(options => configuration.GetSection(QueueConfigurationSection).Bind(options))
                .AddSingleton<IQueueFactory, QueueProviderFactory>()
                .AddSingleton<IQueueProvider, QueueService>();
        }

        public static IServiceCollection AddViajaNetProviders(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddOptions()
                .Configure<QueueConfiguration>(options => configuration.GetSection(QueueConfigurationSection).Bind(options))
                .AddSingleton<IQueueFactory, QueueProviderFactory>()
                .AddSingleton<IQueueProvider, QueueService>();
        }

        public static IServiceCollection AddViajaNetRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddOptions()
                .Configure<QueueConfiguration>(options => configuration.GetSection(QueueConfigurationSection).Bind(options))
                .AddSingleton<IQueueFactory, QueueProviderFactory>()
                .AddSingleton<IQueueProvider, QueueService>();
        }
    }
}
