using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure
{
    public interface IRepositoryQuery
    {
        Task<AnalyticsDto> QueryAsync();
    }
}
