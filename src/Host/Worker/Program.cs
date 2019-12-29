using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Host.Worker
{
    public class Program
    {
        private static readonly AutoResetEvent _locker = new AutoResetEvent(false);
        private static IScheduler _scheduler;

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

        public static async Task Main(string[] args)
        {
            CreateCancellingEvent();

            _scheduler = await SchedulerFactory.CreateAsync();

            await _scheduler.Start();

            var job = JobBuilder.Create<QueueConsumptionJob>()
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
