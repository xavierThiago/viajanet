using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public class QueueProviderFactory : IDisposable, IQueueFactory
    {
        private bool _disposed = false;
        private readonly ConnectionFactory _factory;
        private readonly Lazy<IConnection> _connection;

        public QueueProviderFactory([FromServices] IOptions<QueueConfiguration> options)
        {
            if (options == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(QueueConfiguration)} type from DI container.");
            }

            if (options.Value.Uri != null)
            {
                this._factory = new ConnectionFactory
                {
                    Uri = options.Value.Uri
                };
            }
            else
            {
                if (options.Value.Username == null)
                {
                    throw new ArgumentException("Can not create a queue provider connection without a username.", nameof(options));
                }

                if (options.Value.Password == null)
                {
                    throw new ArgumentException("Can not create a queue provider connection without a password.", nameof(options));
                }

                if (options.Value.VirtualHost == null)
                {
                    throw new ArgumentException("Can not create a queue provider connection without a virtual host.", nameof(options));
                }

                if (options.Value.HostName == null)
                {
                    throw new ArgumentException("Can not create a queue provider connection without a hostname.", nameof(options));
                }

                this._factory = new ConnectionFactory()
                {
                    UserName = options.Value.Username,
                    Password = options.Value.Password,
                    VirtualHost = options.Value.VirtualHost,
                    HostName = options.Value.HostName,
                    UseBackgroundThreadsForIO = true
                };
            }

            this._connection = new Lazy<IConnection>(() =>
            {
                return this._factory.CreateConnection();
            });
        }

        ~QueueProviderFactory() => this.Dispose();

        public IModel CreateChannel()
        {
            if (!this._connection.Value.IsOpen)
            {
                throw new InvalidOperationException("Could not stabilish a connection with the queue provider.");
            }

            return this._connection.Value.CreateModel();
        }

        public void Dispose()
        {
            if (!this._disposed)
            {
                this._connection.Value.Dispose();
                this._disposed = true;
            }
        }
    }
}
