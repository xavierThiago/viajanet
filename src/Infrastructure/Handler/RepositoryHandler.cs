using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViajaNet.JobApplication.Application.Core;
using ViajaNet.JobApplication.Infrastructure.CouchDb;
using ViajaNet.JobApplication.Infrastructure.SqlServer;

namespace ViajaNet.JobApplication.Infrastructure
{
    public class RepositoryHandler : IRepositoryCommand, IRepositoryQuery
    {
        private readonly ICouchDbService _couchDbService;
        private readonly ISqlServerService _sqlServerService;

        public RepositoryHandler([FromServices] ICouchDbService couchDbService,
                                    [FromServices] ISqlServerService sqlServerService)
        {
            if (couchDbService == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(ICouchDbService)} type from DI container.");
            }

            if (sqlServerService == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(ISqlServerService)} type from DI container.");
            }

            this._couchDbService = couchDbService;
            this._sqlServerService = sqlServerService;
        }

        public Task<string> CreateAsync(AnalyticsEntity entity) => this.CreateAsync(entity, CancellationToken.None);

        public async Task<string> CreateAsync(AnalyticsEntity entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await this._sqlServerService.CreateAsync(entity, cancellationToken);

            return await this._couchDbService.CreateAsync(entity, cancellationToken);
        }

        public Task<AnalyticsEntity> QueryByIdAsync(string id) => this.QueryByIdAsync(id, CancellationToken.None);

        public async Task<AnalyticsEntity> QueryByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await this._couchDbService.GetAsync(default);
        }

        public Task<IEnumerable<AnalyticsEntity>> QueryByParametersAsync(string ip, string pageName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AnalyticsEntity>> QueryByParametersAsync(string ip, string pageName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
