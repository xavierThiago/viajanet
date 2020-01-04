using System;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Application.Core
{
    public class VendorDto
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("version")]
        public string Version { get; private set; }

        public VendorDto(string name, string version)
        {
            ValidateInput(name, version);

            this.Name = name;
            this.Version = version;
        }

        private static void ValidateInput(string name, string version)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Name can not be empty.", nameof(name));
            }

            if (version.Length == 0)
            {
                throw new ArgumentException("Version can not be empty.", nameof(version));
            }
        }

        public static VendorDto FromPayload(AnalyticsRequestPayload.VendorRequestPayload vendorPayload)
        {
            if (vendorPayload == null)
            {
                throw new ArgumentNullException(nameof(vendorPayload));
            }

            ValidateInput(vendorPayload.Name, vendorPayload.Version);

            return new VendorDto(vendorPayload.Name, vendorPayload.Version);
        }
    }
}
