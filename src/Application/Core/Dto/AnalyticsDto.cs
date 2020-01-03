using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Application.Core
{
    public class AnalyticsDto
    {
        [JsonProperty("ip")]
        public string IP { get; private set; }

        [JsonProperty("pageName")]
        public string PageName { get; private set; }

        [JsonProperty("vendor")]
        public VendorDto Vendor { get; private set; }

        [JsonProperty("parameters")]
        public Dictionary<string, List<string>> Parameters { get; private set; }

        public AnalyticsDto(string ip, string pageName, VendorDto vendor, Dictionary<string, List<string>> parameters)
        {
            if (ip == null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            if (pageName == null)
            {
                throw new ArgumentNullException(nameof(pageName));
            }

            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IP = ip;
            PageName = pageName;
            Vendor = vendor;
            Parameters = parameters;
        }

        public static AnalyticsDto FromPayload(AnalyticsPayload analyticsPayload)
        {
            if (analyticsPayload == null)
            {
                throw new ArgumentNullException(nameof(analyticsPayload));
            }

            return new AnalyticsDto(analyticsPayload.IP, analyticsPayload.PageName,
                                        VendorDto.FromPayload(analyticsPayload.Vendor),
                                            analyticsPayload.Parameters);
        }
    }
}
