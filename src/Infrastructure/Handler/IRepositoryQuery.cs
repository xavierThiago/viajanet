using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure
{
    public interface IRepositoryQuery
    {
        Task<AnalyticsEntity> QueryByIdAsync(string id);
        Task<AnalyticsEntity> QueryByIdAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<AnalyticsEntity>> QueryByParametersAsync(string ip, string pageName);
        Task<IEnumerable<AnalyticsEntity>> QueryByParametersAsync(string ip, string pageName, CancellationToken cancellationToken);
    }
}
