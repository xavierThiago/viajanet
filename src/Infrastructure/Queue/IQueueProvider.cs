using System.Threading;
using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public interface IQueueProvider
    {
        Task<object> PushAsync<TValue>(string topic, TValue data);
        Task<object> PushAsync<TValue>(string topic, TValue data, CancellationToken cancellationToken);
        Task<object> ShiftAsync(string topic);
        Task<object> ShiftAsync(string topic, CancellationToken cancellationToken);
    }
}
