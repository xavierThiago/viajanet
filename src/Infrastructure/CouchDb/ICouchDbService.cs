using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Core;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure.CouchDb
{
    public interface ICouchDbService : IDisposable
    {
        Task<string> CreateAsync(AnalyticsEntity analyticsEntity);
        Task<string> CreateAsync(AnalyticsEntity analyticsEntity, CancellationToken cancellationToken);
        Task<AnalyticsEntity> GetAsync(string id);
        Task<AnalyticsEntity> GetAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<AnalyticsEntity>> GetAsync(string ip, string pageName);
        Task<IEnumerable<AnalyticsEntity>> GetAsync(string ip, string pageName, CancellationToken cancellationToken);
    }
}
