using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViajaNet.JobApplication.Infrastructure.Queue;

namespace ViajaNet.JobApplication.Application
{
    public class AnalyticsAppService : IAnalyticsAppService
    {
        private readonly IQueueProvider _queueProvider;

        public AnalyticsAppService([FromServices] IQueueProvider queueProvider)
        {
            if (queueProvider == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(queueProvider)} type from DI container.");
            }

            this._queueProvider = queueProvider;
        }

        public Task CreateAsync(AnalyticsDto analyticsDto) => this.CreateAsync(analyticsDto, CancellationToken.None);

        public Task CreateAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<AnalyticsDto>> GetByIdAsync(string id) => this.GetByIdAsync(id, CancellationToken.None);

        public Task<IEnumerable<AnalyticsDto>> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return null;
        }

        public Task<IEnumerable<AnalyticsDto>> GetByIPAsync(string ip) => this.GetByIPAsync(ip, CancellationToken.None);

        public Task<IEnumerable<AnalyticsDto>> GetByIPAsync(string ip, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return null;
        }

        public Task<IEnumerable<AnalyticsDto>> GetByPageNameAsync(string pageName) => this.GetByPageNameAsync(pageName, CancellationToken.None);

        public Task<IEnumerable<AnalyticsDto>> GetByPageNameAsync(string pageName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return null;
        }

        public Task<IEnumerable<AnalyticsDto>> GetByIPAndPageNameAsync(string ip, string pageName) => this.GetByIPAndPageNameAsync(ip, pageName, CancellationToken.None);

        public Task<IEnumerable<AnalyticsDto>> GetByIPAndPageNameAsync(string ip, string pageName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return null;
        }

        public void PushToQueue(string topic, AnalyticsDto analyticsDto)
        {
            throw new NotImplementedException();
        }

        public void ShiftFromQueue(string topic, EventHandler<AnalyticsDto> handler)
        {
            throw new NotImplementedException();
        }
    }
}
