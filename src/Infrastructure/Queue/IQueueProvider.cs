using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public interface IQueueProvider : IDisposable
    {
        void Push<TValue>(TValue data)
            where TValue : class;
        void Push<TValue>(string topic, TValue data)
            where TValue : class;
        void Shift(EventHandler<BasicDeliverEventArgs> handler);
        void Shift(string topic, EventHandler<BasicDeliverEventArgs> handler);
    }
}
