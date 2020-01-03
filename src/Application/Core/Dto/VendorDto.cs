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
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            this.Name = name;
            this.Version = version;
        }

        public static VendorDto FromPayload(AnalyticsPayload.VendorPayload vendorPayload)
        {
            if (vendorPayload == null)
            {
                throw new ArgumentNullException(nameof(vendorPayload));
            }

            return new VendorDto(vendorPayload.Name, vendorPayload.Version);
        }
    }
}
