using Couchbase;
using Couchbase.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
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

        private async Task<string> CreateInternalAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken)
        {
            var document = new Document<AnalyticsDto>
            {
                Id = Guid.NewGuid().ToString(),
                Content = analyticsDto
            };

            cancellationToken.ThrowIfCancellationRequested();

            var upsert = await this._bucket.UpsertAsync(document);

            upsert.EnsureSuccess();

            return upsert.Id;
        }

        private Task GetInternalAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateAsync(AnalyticsDto analyticsDto) => this.CreateAsync(analyticsDto, CancellationToken.None);

        public async Task<string> CreateAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken)
        {
            if (analyticsDto == null)
            {
                throw new ArgumentNullException(nameof(analyticsDto));
            }

            return await this.CreateInternalAsync(analyticsDto, cancellationToken);
        }

        public Task GetAsync(AnalyticsDto analyticsDto) => this.GetAsync(analyticsDto, CancellationToken.None);

        public Task GetAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken)
        {
            if (analyticsDto == null)
            {
                throw new ArgumentNullException(nameof(analyticsDto));
            }

            cancellationToken.ThrowIfCancellationRequested();

            return this.GetInternalAsync(analyticsDto, cancellationToken);
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
