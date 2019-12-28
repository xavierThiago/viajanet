using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ViajaNet.JobApplication.Host.Web.Controllers
{
    public class AnalyticsPayload
    {
        [JsonProperty("ip")]
        public string IP { get; private set; }

        [Required]
        [JsonProperty("pageName")]
        public string PageName { get; set; }

        [Required]
        [JsonProperty("vendor")]
        public VendorPayload Vendor { get; set; }

        /* [JsonProperty("p")]
        public IEnumerable<IDictionary<string, IEnumerable<string>>> P { get; set; } */

        public AnalyticsPayload(string pageName, VendorPayload vendor/* ,
                                    IEnumerable<IDictionary<string, IEnumerable<string>>> parameters */)
        {
            this.PageName = pageName;
            this.Vendor = vendor;
            // this.P = parameters;
        }

        /// <summary>
        /// Adds an IP address to this <see cref="AnalyticsPayload"/> instance.
        /// </summary>
        /// <param name="ip">IP address expressed as <see cref="string"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ip"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="ip"/> is empty.</exception>
        /// <exception cref="FormatException"><paramref name="ip"/> is not valid IP address.</exception>
        public void AddIp(string ip)
        {
            if (ip == null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            if (ip.Length == 0)
            {
                throw new ArgumentException("IP can not be empty.", nameof(ip));
            }

            this.AddIp(IPAddress.Parse(ip));
        }

        /// <summary>
        /// Adds an IP address to this <see cref="AnalyticsPayload"/> instance.
        /// </summary>
        /// <param name="ipAddress">IP address expressed as <see cref="IPAddress"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ipAddress"/> is null.</exception>
        public void AddIp(IPAddress ipAddress)
        {
            if (ipAddress == null)
            {
                throw new ArgumentNullException(nameof(ipAddress));
            }

            this.IP = ipAddress.ToString();
        }

        public class VendorPayload
        {
            private static readonly Regex _versionPattern = new Regex(@"^\d(?:\.\d+)+(\w+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            [Required]
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("version")]
            public string Version { get; set; }

            public VendorPayload(string name, string version)
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (name.Length == 0)
                {
                    throw new ArgumentException($"Name can not be empty.", nameof(name));
                }

                if (version != null && version.Length != 0 &&
                    VendorPayload._versionPattern.Match(version).Success)
                {
                    throw new ArgumentException($"Name can not be empty.", nameof(name));
                }

                this.Name = name;
                this.Version = version;
            }
        }
    }
}
