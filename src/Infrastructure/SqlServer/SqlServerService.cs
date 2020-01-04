using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure.SqlServer
{
    public class SqlServerService : ISqlServerService
    {
        private bool _disposed = false;
        private readonly AnalyticsContext _context;

        public SqlServerService([FromServices] AnalyticsContext analyticsContext)
        {
            if (analyticsContext == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(AnalyticsContext)} type from DI container.");
            }

            this._context = analyticsContext;
        }

        ~SqlServerService() => this.Dispose();

        public Task<string> CreateAsync(AnalyticsEntity analyticsEntity) => this.CreateAsync(analyticsEntity, CancellationToken.None);

        public async Task<string> CreateAsync(AnalyticsEntity analyticsEntity, CancellationToken cancellationToken)
        {
            var result = await this._context.Analytics.AddAsync(analyticsEntity, cancellationToken);

            await this._context.SaveChangesAsync(cancellationToken);

            return string.Empty;
        }

        public void Dispose()
        {
            if (!this._disposed)
            {
                this._disposed = true;

                this._context.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
