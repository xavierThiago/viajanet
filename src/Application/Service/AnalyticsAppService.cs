using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViajaNet.JobApplication.Application.Core;
using ViajaNet.JobApplication.Infrastructure;
using ViajaNet.JobApplication.Infrastructure.Queue;
using System.Text;
using Newtonsoft.Json;

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

        public Task<string> CreateAsync(AnalyticsDto analyticsDto) => this.CreateAsync(analyticsDto, CancellationToken.None);

        public async Task<string> CreateAsync(AnalyticsDto analyticsDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await this._commandHandler.CreateAsync(analyticsDto.ToEntity()); ;
        }

        public Task<AnalyticsDto> GetByIdAsync(string id) => this.GetByIdAsync(id, CancellationToken.None);

        public async Task<AnalyticsDto> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await this._queryHandler.QueryByIdAsync(id)).ToDto();
        }

        public Task<IEnumerable<AnalyticsDto>> GetByIPAsync(string ip) => this.GetByIPAsync(ip, CancellationToken.None);

        public async Task<IEnumerable<AnalyticsDto>> GetByIPAsync(string ip, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await this._queryHandler.QueryByParametersAsync(ip, null, cancellationToken)).Select(x => x.ToDto());
        }

        public Task<IEnumerable<AnalyticsDto>> GetByPageNameAsync(string pageName) => this.GetByPageNameAsync(pageName, CancellationToken.None);

        public async Task<IEnumerable<AnalyticsDto>> GetByPageNameAsync(string pageName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await this._queryHandler.QueryByParametersAsync(null, pageName, cancellationToken)).Select(x => x.ToDto());
        }

        public Task<IEnumerable<AnalyticsDto>> GetByIPAndPageNameAsync(string ip, string pageName) => this.GetByIPAndPageNameAsync(ip, pageName, CancellationToken.None);

        public async Task<IEnumerable<AnalyticsDto>> GetByIPAndPageNameAsync(string ip, string pageName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await this._queryHandler.QueryByParametersAsync(ip, pageName, cancellationToken)).Select(x => x.ToDto());
        }

        public void PushToQueue(string topic, AnalyticsDto analyticsDto) => this._queueProvider.Push<AnalyticsDto>(topic, analyticsDto);

        public void ShiftFromQueue(string topic, Action<AnalyticsDto> handler)
        {
            this._queueProvider.Shift(topic, (sender, e) =>
            {
                var json = Encoding.UTF8.GetString(e.Body);
                var hit = JsonConvert.DeserializeObject<AnalyticsDto>(json);

                handler(hit);
            });
        }
    }
}
