using System;
using RabbitMQ.Client;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public interface IQueueFactory : IDisposable
    {
        IModel CreateChannel();
    }
}
