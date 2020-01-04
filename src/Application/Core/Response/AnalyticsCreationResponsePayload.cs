using System;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Application.Core
{
    /// <summary>
    /// Create an instance of <see cref="AnalyticsCreationResponsePayload"/>.
    /// </summary>
    public class AnalyticsCreationResponsePayload : IApiResponse
    {
        /// <summary>
        /// Client identification.
        /// </summary>
        /// <value>Identification comes from database.</value>
        [JsonProperty("id")]
        public long Id { get; private set; }

        public AnalyticsCreationResponsePayload(long id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id can not be less than zero.", nameof(id));
            }

            this.Id = id;
        }
    }
}
