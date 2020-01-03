using System;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Core;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure.CouchDb
{
    public interface ICouchDbService : IDisposable
    {
        Task<string> CreateAsync(AnalyticsDto analyticsDto);
        Task<string> CreateAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken);
        Task GetAsync(AnalyticsDto analyticsDto);
        Task GetAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken);
    }
}
