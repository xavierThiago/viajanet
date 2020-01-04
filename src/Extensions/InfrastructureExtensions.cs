using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViajaNet.JobApplication.Application.Core;
using ViajaNet.JobApplication.Infrastructure;
using ViajaNet.JobApplication.Infrastructure.CouchDb;
using ViajaNet.JobApplication.Infrastructure.Queue;
using Microsoft.EntityFrameworkCore;
using ViajaNet.JobApplication.Infrastructure.SqlServer;

namespace ViajaNet.JobApplication.Extensions
{
    public static class InfrastructureExtensions
    {
        private const string QueueConfigurationSection = "PubSub:RabbitMq";
        private const string CouchDbConfigurationSection = "Repository:CouchDb";

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
                .AddTransient<IQueueProvider, QueueService>();
        }

        public static IServiceCollection AddViajaNetRepositories(this IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json", optional: true);

            var configuration = configBuilder.Build();

            return services.AddOptions()
                .Configure<CouchDbConfiguration>(options => configuration.GetSection(CouchDbConfigurationSection).Bind(options))
                .AddSingleton<ICouchDbFactory, CouchDbFactory>()
                .AddTransient<ICouchDbService, CouchDbService>()
                .AddDbContext<AnalyticsContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")))
                .AddSingleton<IRepositoryCommand, RepositoryHandler>()
                .AddSingleton<IRepositoryQuery, RepositoryHandler>();
        }
    }
}
