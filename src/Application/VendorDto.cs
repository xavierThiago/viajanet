using System;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Application
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
    }
}
