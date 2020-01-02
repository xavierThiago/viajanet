using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public class QueueProviderFactory : IDisposable
    {
        private bool _disposed = false;
        private readonly IConnection _connection;

        public QueueProviderFactory([FromServices] IOptions<QueueConfiguration> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var connection = default(ConnectionFactory);

            if (options.Value.Uri != null)
            {
                connection = new ConnectionFactory
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

                connection = new ConnectionFactory()
                {
                    UserName = options.Value.Username,
                    Password = options.Value.Password,
                    VirtualHost = options.Value.VirtualHost,
                    HostName = options.Value.HostName,
                    UseBackgroundThreadsForIO = true
                };
            }

            this._connection = connection.CreateConnection();

            if (!this._connection.IsOpen)
            {
                throw new InvalidOperationException("Could not stabilish a connection with the queue provider.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                this._connection.Dispose();
                this._disposed = true;
            }
        }

        public IModel CreateChannel() => this._connection.CreateModel();

        public void Dispose() => this.Dispose(true);

        ~QueueProviderFactory()
        {
            Dispose(false);
        }
    }
}
