using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Host.Api
{
    public sealed class SuccessResult<TResult>
        where TResult : class
    {
        [JsonProperty("status")]
        public bool Status { get; } = true;

        [JsonProperty("message")]
        public string Message { get; }

        [JsonProperty("result")]
        public TResult Result { get; }

        public SuccessResult(string message, TResult result)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            this.Message = message;
            this.Result = result;
        }
    }
}
