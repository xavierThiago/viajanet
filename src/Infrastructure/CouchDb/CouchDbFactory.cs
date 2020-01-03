using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Infrastructure.CouchDb
{
    public class CouchDbFactory : ICouchDbFactory, ICouchDbFactoryAsync
    {
        private bool _disposed = false;
        protected readonly Lazy<ICluster> _cluster;
        protected readonly IBucket _bucket;
        protected readonly string _bucketName;

        public CouchDbFactory([FromServices] IOptions<CouchDbConfiguration> options)
        {
            if (options == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(CouchDbConfiguration)} type from DI container.");
            }

            var cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    options.Value.ConnectionString
                }
            });

            var authenticator = new PasswordAuthenticator(options.Value.Username, options.Value.Password);

            this._bucketName = options.Value.Bucket;
            this._cluster = new Lazy<ICluster>(() =>
            {
                cluster.Authenticate(authenticator);

                return cluster;
            });
        }

        ~CouchDbFactory() => this.Dispose();

        public IBucket CreateBucket()
        {
            if (!this._cluster.Value.IsOpen(this._bucketName))
            {
                throw new InvalidOperationException("Could not stabilish a CouchDb connection.");
            }

            return this._cluster.Value.OpenBucket();
        }

        public Task<IBucket> CreateBucketAsync() => this._cluster.Value.OpenBucketAsync();

        public void Dispose()
        {
            if (!this._disposed)
            {
                this._disposed = true;

                this._bucket.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
