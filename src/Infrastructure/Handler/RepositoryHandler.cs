using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViajaNet.JobApplication.Application.Core;
using ViajaNet.JobApplication.Infrastructure.CouchDb;

namespace ViajaNet.JobApplication.Infrastructure
{
    public class RepositoryHandler : IRepositoryCommand, IRepositoryQuery
    {
        private readonly ICouchDbService _couchDbService;
        // private readonly ISqlServerService _sqlServerService;

        public RepositoryHandler([FromServices] ICouchDbService couchDbService/* ,
                                    [FromServices] ISqlServerService sqlServerService */)
        {
            if (couchDbService == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(ICouchDbService)} type from DI container.");
            }

            /* if (sqlServerService == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(ISqlServerService)} type from DI container.");
            } */

            this._couchDbService = couchDbService;
            // this._sqlServerService = sqlServerService;
        }

        public Task<string> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AnalyticsDto> QueryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
