using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Application.Core
{
    public class AnalyticsDto
    {
        [JsonProperty("Id")]
        public long Id { get; private set; }

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
            if (vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            ValidateInput(ip, pageName, vendor.Name,
                            vendor.Version);

            this.IP = ip;
            this.PageName = pageName;
            this.Vendor = vendor;
            this.Parameters = parameters;
        }

        private static void ValidateInput(string ip, string pageName,
                                            string vendorName, string vendorVersion)
        {
            if (ip == null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            if (pageName == null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            if (vendorName == null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            if (vendorVersion == null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            if (ip.Length == 0)
            {
                throw new ArgumentException("IP can not be empty.", nameof(ip));
            }

            if (pageName.Length == 0)
            {
                throw new ArgumentException("Page name can not be empty.", nameof(ip));
            }

            if (vendorName.Length == 0)
            {
                throw new ArgumentException("Vendor name can not be empty.", nameof(ip));
            }

            if (vendorVersion.Length == 0)
            {
                throw new ArgumentException("Vendor version can not be empty.", nameof(ip));
            }
        }

        public static AnalyticsDto FromPayload(AnalyticsRequestPayload analyticsPayload)
        {
            if (analyticsPayload == null)
            {
                throw new ArgumentNullException(nameof(analyticsPayload));
            }

            ValidateInput(analyticsPayload.IP, analyticsPayload.PageName,
                            analyticsPayload.Vendor.Name, analyticsPayload.Vendor.Version);

            return new AnalyticsDto(analyticsPayload.IP, analyticsPayload.PageName,
                                        VendorDto.FromPayload(analyticsPayload.Vendor),
                                            analyticsPayload.Parameters);
        }

        public AnalyticsResponsePayload ToResponse()
        {
            return new AnalyticsResponsePayload(this.Id, this.IP, this.PageName,
                                                    new AnalyticsResponsePayload.VendorResponsePayload(this.Vendor.Name,
                                                                                                            this.Vendor.Version),
                                                                                                                 this.Parameters);
        }
        public AnalyticsEntity ToEntity()
        {
            return new AnalyticsEntity(this.IP, this.PageName,
                                        this.Vendor.Name, this.Vendor.Version,
                                            this.Parameters);
        }
    }
}
