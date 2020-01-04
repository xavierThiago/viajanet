using System;
using System.Collections.Generic;

namespace ViajaNet.JobApplication.Application.Core
{
    public class AnalyticsEntity
    {
        public long Id { get; private set; }

        public string IP { get; private set; }

        public string PageName { get; private set; }

        public string VendorName { get; private set; }

        public string VendorVersion { get; private set; }

        public Dictionary<string, List<string>> Parameters { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        private AnalyticsEntity()
        { }

        public AnalyticsEntity(string ip, string pageName,
                                    string vendorName, string vendorVersion,
                                        Dictionary<string, List<string>> parameters)
        {
            ValidateInput(ip, pageName,
                            vendorName, vendorVersion);

            this.IP = ip;
            this.PageName = pageName;
            this.VendorName = vendorName;
            this.VendorVersion = vendorVersion;
            this.Parameters = parameters;
        }

        public AnalyticsEntity(long id, string ip, string pageName,
                                string vendorName, string vendorVersion,
                                    Dictionary<string, List<string>> parameters,
                                        DateTimeOffset createdAt)
        {
            ValidateInput(id, ip, pageName,
                            vendorName, vendorVersion,
                                createdAt);

            this.Id = id;
            this.IP = ip;
            this.PageName = pageName;
            this.VendorName = vendorName;
            this.VendorVersion = vendorVersion;
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
                throw new ArgumentNullException(nameof(pageName));
            }

            if (vendorName == null)
            {
                throw new ArgumentNullException(nameof(vendorName));
            }

            if (vendorVersion == null)
            {
                throw new ArgumentNullException(nameof(vendorVersion));
            }

            if (ip.Length == 0)
            {
                throw new ArgumentException("IP can not be empty.", nameof(ip));
            }

            if (pageName.Length == 0)
            {
                throw new ArgumentException("Page name can not be empty.", nameof(pageName));
            }

            if (vendorName.Length == 0)
            {
                throw new ArgumentException("Vendor name can not be empty.", nameof(vendorName));
            }

            if (vendorVersion.Length == 0)
            {
                throw new ArgumentException("Vendor version can not be empty.", nameof(vendorVersion));
            }
        }

        private static void ValidateInput(long id, string ip, string pageName,
                                            string vendorName, string vendorVersion,
                                                DateTimeOffset createdAt)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id can not be less than zero.", nameof(id));
            }

            ValidateInput(ip, pageName, vendorName, vendorVersion);

            if (createdAt > DateTimeOffset.Now)
            {
                throw new ArgumentException("Created at can no be greater than today.", nameof(createdAt));
            }
        }

        public AnalyticsDto ToDto()
        {
            return new AnalyticsDto(this.IP, this.PageName,
                                        new VendorDto(this.VendorName, this.VendorVersion),
                                            this.Parameters);
        }

        public static AnalyticsEntity FromDto(AnalyticsDto analyticsDto)
        {
            if (analyticsDto == null)
            {
                throw new ArgumentNullException(nameof(analyticsDto));
            }

            ValidateInput(analyticsDto.IP, analyticsDto.PageName,
                            analyticsDto.Vendor.Name, analyticsDto.Vendor.Version);

            return new AnalyticsEntity(analyticsDto.IP, analyticsDto.PageName,
                                        analyticsDto.Vendor.Name, analyticsDto.Vendor.Version,
                                            analyticsDto.Parameters);
        }
    }
}
