using System.Collections.Generic;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Application
{
    public class AnalyticsDto
    {
        [JsonProperty("ip")]
        public string IP { get; private set; }

        [JsonProperty("pageName")]
        public string PageName { get; private set; }

        [JsonProperty("vendor")]
        public VendorDto Vendor { get; private set; }

        public Dictionary<string, List<string>> Parameters { get; private set; }

        /* public static CreateFromPayload(AnalyticsPayload payloadDto)
        {

        } */
    }
}
