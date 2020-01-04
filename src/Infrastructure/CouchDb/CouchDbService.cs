using Couchbase;
using Couchbase.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure.CouchDb
{
    public class CouchDbService : ICouchDbService
    {
        private bool _disposed = false;
        private readonly IBucket _bucket;

        public CouchDbService([FromServices] ICouchDbFactory factory)
        {
            if (factory == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(factory)} type from DI container.");
            }

            this._bucket = factory.CreateBucket();
        }

        ~CouchDbService() => this.Dispose();

        private async Task<string> CreateInternalAsync(AnalyticsEntity analyticsEntity, CancellationToken cancellationToken)
        {
            var document = new Document<AnalyticsEntity>
            {
                Id = Guid.NewGuid().ToString(),
                Content = analyticsEntity
            };

            cancellationToken.ThrowIfCancellationRequested();

            var upsert = await this._bucket.UpsertAsync(document);

            upsert.EnsureSuccess();

            return upsert.Id;
        }

        private async Task<AnalyticsEntity> GetByIdInternalAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await this._bucket.GetDocumentAsync<AnalyticsEntity>(id)).Document.Content;
        }

        private async Task<IEnumerable<AnalyticsEntity>> GetByParametersInternalAsync(string ip, string pageName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Need more time to learn CouchDb.
            return (await this._bucket.QueryAsync<AnalyticsEntity>("")).Rows;
        }

        public Task<string> CreateAsync(AnalyticsEntity analyticsEntity) => this.CreateAsync(analyticsEntity, CancellationToken.None);

        public async Task<string> CreateAsync(AnalyticsEntity analyticsEntity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (analyticsEntity == null)
            {
                throw new ArgumentNullException(nameof(analyticsEntity));
            }

            return await this.CreateInternalAsync(analyticsEntity, cancellationToken);
        }

        public Task<AnalyticsEntity> GetAsync(string id) => this.GetAsync(id, CancellationToken.None);

        public Task<AnalyticsEntity> GetAsync(string id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            cancellationToken.ThrowIfCancellationRequested();

            return this.GetByIdInternalAsync(id, cancellationToken);
        }

        public Task<IEnumerable<AnalyticsEntity>> GetAsync(string ip, string pageName) => this.GetAsync(ip, pageName, CancellationToken.None);

        public Task<IEnumerable<AnalyticsEntity>> GetAsync(string ip, string pageName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this.GetByParametersInternalAsync(ip, pageName, cancellationToken);
        }

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
