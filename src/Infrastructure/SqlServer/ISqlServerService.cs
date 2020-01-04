using System;
using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure.SqlServer
{
    public interface ISqlServerService : IDisposable
    {
        Task<string> CreateAsync(AnalyticsEntity analyticsEntity);
        Task<string> CreateAsync(AnalyticsEntity analyticsEntity, CancellationToken cancellationToken);
    }
}
