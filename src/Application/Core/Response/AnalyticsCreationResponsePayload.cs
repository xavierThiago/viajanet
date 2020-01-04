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
        public string Id { get; private set; }

        public AnalyticsCreationResponsePayload(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Id = id;
        }
    }
}
