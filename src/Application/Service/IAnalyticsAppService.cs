using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Application.Service
{
    public interface IAnalyticsAppService
    {
        Task CreateAsync(AnalyticsDto analyticsDto);
        Task CreateAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken);
        Task<IEnumerable<AnalyticsDto>> GetByIdAsync(string id);
        Task<IEnumerable<AnalyticsDto>> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<AnalyticsDto>> GetByIPAsync(string ip);
        Task<IEnumerable<AnalyticsDto>> GetByIPAsync(string ip, CancellationToken cancellationToken);
        Task<IEnumerable<AnalyticsDto>> GetByPageNameAsync(string pageName);
        Task<IEnumerable<AnalyticsDto>> GetByPageNameAsync(string pageName, CancellationToken cancellationToken);
        Task<IEnumerable<AnalyticsDto>> GetByIPAndPageNameAsync(string ip, string pageName);
        Task<IEnumerable<AnalyticsDto>> GetByIPAndPageNameAsync(string ip, string pageName, CancellationToken cancellationToken);

        void PushToQueue(string topic, AnalyticsDto analyticsDto);
        void ShiftFromQueue(string topic, EventHandler<AnalyticsDto> handler);
    }
}