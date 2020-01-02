using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Infrastructure.Queue;
using RabbitMQ.Client;
using System.IO;
using System.Collections.Generic;

namespace ViajaNet.JobApplication.Host.Worker
{
    public class Program
    {
        private static readonly AutoResetEvent _locker = new AutoResetEvent(false);
        private static IScheduler _scheduler;

        private static IServiceProvider ConfigureContainer()
        {
            var configBuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.{"Development"}.json", optional: true);
            var configuration = configBuilder.Build();

            return new ServiceCollection()
                            .AddOptions()
                            .Configure<QueueConfiguration>(options => configuration.GetSection("PubSub:RabbitMq").Bind(options))
                            .AddSingleton<IQueueFactory, QueueProviderFactory>()
                            .AddSingleton<IQueueProvider, QueueService>()
                            .BuildServiceProvider();
        }

        private static void ConfigureEvents()
        {
            Console.CancelKeyPress += async (sender, e) =>
            {
                e.Cancel = true;

                await Console.Out.WriteLineAsync("\nWaiting for pending jobs to complete...");

                // Wait for any job completion first.
                await _scheduler.Shutdown(true);

                // Releasing the main thread.
                _locker.Set();

                await Console.Out.WriteLineAsync("Done.");
            };
        }

        public static async Task Main(string[] args)
        {
            ConfigureEvents();

            var container = ConfigureContainer();
            var queue = container.GetService<IQueueProvider>();

            _scheduler = await SchedulerFactory.CreateAsync();

            await _scheduler.Start();

            var job = JobBuilder.Create<QueueConsumptionJob>()
                .UsingJobData(new JobDataMap(new Dictionary<string, IQueueProvider>
                {
                    {"queueProvider", queue}
                }))
                .WithIdentity("consumption-job", "queue")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("consumption-trigger", "queue")
                .WithCronSchedule(QueueConsumptionJob.CronTrigger)
                .Build();

            await _scheduler.ScheduleJob(job, trigger);

            await Console.Out.WriteLineAsync("Queue consumption started.");

            _locker.WaitOne();
        }
    }
}
