using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViajaNet.JobApplication.Application.Core;
using ViajaNet.JobApplication.Infrastructure;
using ViajaNet.JobApplication.Infrastructure.Queue;

namespace ViajaNet.JobApplication.Application.Service
{
    public class AnalyticsAppService : IAnalyticsAppService
    {
        private readonly IQueueProvider _queueProvider;
        private readonly IRepositoryCommand _commandHandler;
        private readonly IRepositoryQuery _queryHandler;

        public AnalyticsAppService([FromServices] IQueueProvider queueProvider,
                                        [FromServices] IRepositoryCommand commandHandler,
                                            [FromServices] IRepositoryQuery queryHandler)
        {
            if (queueProvider == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(IQueueProvider)} type from DI container.");
            }

            if (commandHandler == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(IRepositoryCommand)} type from DI container.");
            }

            if (queryHandler == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(IRepositoryQuery)} type from DI container.");
            }

            this._queueProvider = queueProvider;
            this._commandHandler = commandHandler;
            this._queryHandler = queryHandler;
        }

        public Task<long> CreateAsync(AnalyticsDto analyticsDto) => this.CreateAsync(analyticsDto, CancellationToken.None);

        public async Task<long> CreateAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            long id = await this._commandHandler.CreateAsync(analyticsDto.ToEntity());

            return id;
        }

        public Task<AnalyticsDto> GetByIdAsync(string id) => this.GetByIdAsync(id, CancellationToken.None);

        public Task<AnalyticsDto> GetByIdAsync(string id, CancellationToken cancellationToken)
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
