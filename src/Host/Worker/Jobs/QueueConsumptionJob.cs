using System;
using System.Threading.Tasks;
using Quartz;
using ViajaNet.JobApplication.Application.Service;

namespace ViajaNet.JobApplication.Host.Worker
{
    /// <summary>
    /// Job that consumes a queue provider and save its information on a database.
    /// </summary>
    /// <remarks><see cref="IScheduler"/> will use it to perform consumption of queue provider.</remarks>
    public class QueueConsumptionJob : IJob
    {
        /// <summary>
        /// Job trigger information.
        /// </summary>
        /// <remarks>Every ten seconds, between 09:00 and 18:00, everyday.</remarks>
        public const string CronTrigger = "0/10 * 09-18 * * ?";

        /// <summary>
        /// Job process that <see cref="Quartz.IScheduler"/> will execute.
        /// </summary>
        /// <param name="context">Current job runtime information.</param>
        /// <returns>A <see cref="Task"/> of the current job execution.</returns>
        public Task Execute(IJobExecutionContext context)
        {
            if (context.MergedJobDataMap.TryGetValue("appService", out object appService))
            {
                var service = appService as IAnalyticsAppService;

                if (service == null)
                {
                    _ = Console.Out.WriteLineAsync($"Job \"{context.JobDetail.Key}\" started.");

                    return Task.CompletedTask;
                }

                service.ShiftFromQueue("analytics", async (hit) =>
                {
                    await service.CreateAsync(hit);

                    _ = Console.Out.WriteAsync("Queue consumption succeeded.");
                });
            }
            else
            {
                _ = Console.Out.WriteLineAsync($"Job \"{context.JobDetail.Key}\" started.");
            }

            return Task.CompletedTask;
        }
    }
}
