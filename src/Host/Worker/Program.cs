using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Host.Worker
{
    public class Program
    {
        private static readonly AutoResetEvent _locker = new AutoResetEvent(false);
        private static IScheduler _scheduler;

        public static async Task Main(string[] args)
        {
            CreateCancellingEvent();

            _scheduler = await CreateSchedulerAsync();

            await _scheduler.Start();

            var job = JobBuilder.Create<QueueConsumptionJob>()
                .WithIdentity("consumption-job", "queue")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("consumption-trigger", "queue")
                .StartNow()
                .WithCronSchedule(QueueConsumptionJob.CronTrigger)
                .Build();

            // Tell quartz to schedule the job using our trigger
            await _scheduler.ScheduleJob(job, trigger);
            await Console.Out.WriteLineAsync("Queue consumption started.");

            _locker.WaitOne();
        }

        private static Task<IScheduler> CreateSchedulerAsync()
        {
            // Initialization properties.
            var factory = new StdSchedulerFactory(new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                });

            return factory.GetScheduler();
        }

        private static void CreateCancellingEvent()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;

                Console.Out.WriteLine("\nWaiting for pending jobs to complete...");

                // Wait for any job completion first.
                _scheduler.Shutdown(true);
                _locker.Set();

                Console.Out.WriteLine("Done.");
            };
        }
    }
}
