using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace ViajaNet.JobApplication.Application.Core
{
    /// <summary>
    /// Create an instance of <see cref="AnalyticsRequestPayload"/>.
    /// </summary>
    /// <remarks>Analytics API receives it as a payload.</remarks>
    public class AnalyticsRequestPayload : IValidatableObject
    {
        /// <summary>
        /// Client IP.
        /// </summary>
        /// <value>IP information is collected on server.</value>
        [JsonProperty("ip")]
        public string IP { get; private set; }

        /// <summary>
        /// Page name.
        /// </summary>
        /// <value>HTML title retrieved from the client.</value>
        [Required]
        [JsonProperty("pageName")]
        public string PageName { get; set; }

        /// <summary>
        /// Vendor information
        /// </summary>
        /// <value>Contains browser's name and version.</value>
        [Required]
        [JsonProperty("vendor")]
        public VendorRequestPayload Vendor { get; set; }

        /// <summary>
        /// Client parameters
        /// </summary>
        /// <value>Parameters from client's query string.</value>
        [JsonProperty("parameters")]
        public Dictionary<string, List<string>> Parameters { get; set; }

        /// <summary>
        /// Create an instance of <see cref="AnalyticsRequestPayload"/>, with a <paramref name="pageName"/> and <paramref name="vendor"/>.
        /// </summary>
        /// <param name="pageName">Page name.</param>
        /// <param name="vendor">Browser information.</param>
        public AnalyticsRequestPayload(string pageName, VendorRequestPayload vendor,
                                                    Dictionary<string, List<string>> parameters)
        {
            this.PageName = pageName;
            this.Vendor = vendor;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Adds an IP address to this <see cref="AnalyticsRequestPayload"/> instance.
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
        /// Adds an IP address to this <see cref="AnalyticsRequestPayload"/> instance.
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();

        public AnalyticsDto ToDto()
        {
            return new AnalyticsDto(this.IP, this.PageName,
                                        new VendorDto(this.Vendor.Name, this.Vendor.Version),
                                            this.Parameters);
        }

        /// <summary>
        /// Create an instance of <see cref="VendorRequestPayload"/>.
        /// </summary>
        /// <remarks>Vendor information inside analytics payload.</remarks>
        public class VendorRequestPayload
        {
            private static readonly Regex _versionPattern = new Regex(@"^\d(?:\.\d+)+(\w+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            /// <summary>
            /// Name.
            /// </summary>
            /// <value>Browser name.</value>
            [Required]
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Version.
            /// </summary>
            /// <value>Browser version.</value>
            [Required]
            [JsonProperty("version")]
            public string Version { get; set; }

            /// <summary>
            /// Create an instance of <see cref="VendorRequestPayload"/> with a <paramref name="name"/> and <paramref name="version"/>.
            /// </summary>
            /// <param name="name">Browser name.</param>
            /// <param name="version">Browser version.</param>
            public VendorRequestPayload(string name, string version)
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
                    VendorRequestPayload._versionPattern.Match(version).Success)
                {
                    throw new ArgumentException($"Name can not be empty.", nameof(name));
                }

                this.Name = name;
                this.Version = version;
            }
        }
    }
}
