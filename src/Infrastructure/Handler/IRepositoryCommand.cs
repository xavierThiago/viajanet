using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure
{
    public interface IRepositoryCommand
    {
        Task<long> CreateAsync(AnalyticsEntity entity);
        Task<long> CreateAsync(AnalyticsEntity entity, CancellationToken cancellationToken);
    }
}
