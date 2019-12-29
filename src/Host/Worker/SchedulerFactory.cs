
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
