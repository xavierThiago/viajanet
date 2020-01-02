using System;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Infrastructure.Queue
{
    public class QueueConfiguration
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("VirtualHost")]
        public string VirtualHost { get; set; }

        [JsonProperty("HostName")]
        public string HostName { get; set; }

        [JsonProperty("Uri")]
        public Uri Uri { get; set; }
    }
}
