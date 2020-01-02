using RabbitMQ.Client;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public interface IQueueFactory
    {
        IModel CreateChannel();
    }
}
