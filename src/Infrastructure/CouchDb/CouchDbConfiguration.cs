using System;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Infrastructure.CouchDb
{
    public class CouchDbConfiguration
    {
        [JsonProperty("Host")]
        public string Host { get; set; }

        [JsonProperty("Port")]
        public string Port { get; set; }

        [JsonProperty("Bucket")]
        public string Bucket { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConnectionString")]
        public Uri ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Host) &&
                        !string.IsNullOrEmpty(this.Port))
                {
                    return new Uri($"http://{this.Host}:{this.Port}/pools");
                }

                return default;
            }
        }
    }
}
