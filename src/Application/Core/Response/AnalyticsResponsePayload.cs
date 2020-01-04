using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViajaNet.JobApplication.Application.Core
{
    /// <summary>
    /// Create an instance of <see cref="AnalyticsResponsePayload"/>.
    /// </summary>
    /// <remarks>Analytics API receives it as a payload.</remarks>
    public class AnalyticsResponsePayload : IApiResponse
    {
        /// <summary>
        /// Client IP.
        /// </summary>
        /// <value>IP information is collected on server.</value>
        [JsonProperty("id")]
        public long Id { get; private set; }

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
        public VendorResponsePayload Vendor { get; set; }

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
        public AnalyticsResponsePayload(long id, string ip,
                                            string pageName, VendorResponsePayload vendor,
                                                Dictionary<string, List<string>> parameters)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id can not be less than zero.", nameof(id));
            }

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

            if (vendor.Name == null)
            {
                throw new ArgumentNullException(nameof(vendor.Name));
            }

            if (vendor.Version == null)
            {
                throw new ArgumentNullException(nameof(vendor.Version));
            }

            if (ip.Length == 0)
            {
                throw new ArgumentException("IP can not be empty.", nameof(ip));
            }

            if (pageName.Length == 0)
            {
                throw new ArgumentException("Page name can not be empty.", nameof(pageName));
            }

            if (vendor.Name.Length == 0)
            {
                throw new ArgumentException("Vendor name can not be empty.", nameof(vendor.Name));
            }

            if (vendor.Version.Length == 0)
            {
                throw new ArgumentException("Vendor version can not be empty.", nameof(vendor.Version));
            }

            this.Id = id;
            this.PageName = pageName;
            this.Vendor = vendor;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Create an instance of <see cref="VendorPayload"/>.
        /// </summary>
        /// <remarks>Vendor information inside analytics payload.</remarks>
        public class VendorResponsePayload
        {
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
            [JsonProperty("version")]
            public string Version { get; set; }

            /// <summary>
            /// Create an instance of <see cref="VendorPayload"/> with a <paramref name="name"/> and <paramref name="version"/>.
            /// </summary>
            /// <param name="name">Browser name.</param>
            /// <param name="version">Browser version.</param>
            public VendorResponsePayload(string name, string version)
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (name.Length == 0)
                {
                    throw new ArgumentException($"Name can not be empty.", nameof(name));
                }

                this.Name = name;
                this.Version = version;
            }
        }
    }
}
