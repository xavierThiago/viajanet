using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure
{
    public interface IRepositoryCommand
    {
        Task<string> CreateAsync(AnalyticsEntity entity);
        Task<string> CreateAsync(AnalyticsEntity entity, CancellationToken cancellationToken);
    }
}
