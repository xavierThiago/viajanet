using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public class QueueService : IQueueProvider
    {
        private readonly IModel _channel;
        private readonly PublicationAddress _address;
        private readonly IBasicProperties _properties;
        private readonly bool _queueDeclared = false;
        private bool _disposed = false;

        public QueueService([FromServices] IQueueFactory factory)
        {
            if (factory == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(factory)} type from DI container.");
            }

            var channel = factory.CreateChannel();

            if (!channel.IsOpen)
            {
                throw new InvalidOperationException($"Channel were not previously conected.");
            }

            this._channel = channel;
            this._properties = this._channel.CreateBasicProperties();
            this._address = new PublicationAddress(string.Empty, string.Empty, string.Empty);
        }

        ~QueueService() => this.Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                this._channel.Dispose();
                this._disposed = true;
            }
        }

        public void Push<TValue>(TValue data)
            where TValue : class, new()
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            this.Push(string.Empty, data);
        }

        public void Push<TValue>(string topic, TValue data)
            where TValue : class, new()
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!this._queueDeclared)
            {
                this._channel.QueueDeclare(queue: topic,
                                            durable: true,
                                                exclusive: true,
                                                    autoDelete: false,
                                                        arguments: null);
            }

            this._channel.BasicPublish(this._address, this._properties,
                                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        public void Shift(EventHandler<BasicDeliverEventArgs> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            this.Shift(string.Empty, handler);
        }

        public void Shift(string topic, EventHandler<BasicDeliverEventArgs> handler)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var consumer = new EventingBasicConsumer(this._channel);
            consumer.Received += handler;

            this._channel.BasicConsume(topic, true, consumer);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
