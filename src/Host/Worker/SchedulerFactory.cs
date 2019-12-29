
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Host.Worker
{
    /// <summary>
    /// Job that consumes a queue provider and save its information on a database.
    /// </summary>
    /// <remarks><see cref="IScheduler"/> will use it to perform consumption of queue provider.</remarks>
    public static class SchedulerFactory
    {
        /// <summary>
        /// Creates a <see cref="IScheduler"/> instance with basic configuration.
        /// </summary>
        /// <returns>A <see cref="Task{IScheduler}"/> containing the scheduler handler.</returns>
        public static Task<IScheduler> CreateAsync()
        {
            // Initialization properties.
            var factory = new StdSchedulerFactory(new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                });

            return factory.GetScheduler();
        }
    }
}
