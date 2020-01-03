using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Infrastructure.Queue;
using ViajaNet.JobApplication.Extensions;
using ViajaNet.JobApplication.Application;

namespace ViajaNet.JobApplication.Host.Worker
{
    public class Program
    {
        private static readonly AutoResetEvent _locker = new AutoResetEvent(false);
        private static IScheduler _scheduler;

        private static IAnalyticsAppService CreateAppServiceContainer() => new ServiceCollection()
                                                                            .AddViajaNetApplication()
                                                                            .BuildServiceProvider()
                                                                            .GetService<IAnalyticsAppService>();

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

            var service = CreateAppServiceContainer();

            _scheduler = await SchedulerFactory.CreateAsync();

            await _scheduler.Start();

            var job = JobBuilder.Create<QueueConsumptionJob>()
                .WithIdentity("consumption-job", "queue")
                .UsingJobData(new JobDataMap(new Dictionary<string, IAnalyticsAppService>
                {
                    {"appService", service}
                }))
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
