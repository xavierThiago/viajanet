using System.Collections.Generic;
using System.Net;
using ViajaNet.JobApplication.Host.Api;
using Xunit;

namespace ViajaNet.JobApplication.Tests
{
    public class AnalyticsPayloadTests
    {
        [Fact]
        public void Validate_New_Instance_Creates_Object_Successfully()
        {
            //Arrange
            const string PageName = "ViajaNet - Checkout";
            const string VendorName = "Chrome";
            const string VendorVersion = "58.34.43.2019";

            var parameters = new Dictionary<string, List<string>>
            {
                { "bar", new List<string> { "true" } }
            };

            //Act
            var vendor = new AnalyticsPayload.VendorPayload(VendorName, VendorVersion);
            var payload = new AnalyticsPayload(PageName, vendor, parameters);

            //Assert
            Assert.NotNull(vendor);
            Assert.NotNull(payload);
            Assert.Equal(PageName, payload.PageName);
            Assert.Equal(VendorName, payload.Vendor.Name);
            Assert.Equal(VendorVersion, payload.Vendor.Version);
        }

        [Fact]
        public void Validate_Adding_IP_To_Instance_Success()
        {
            //Arrange
            const string PageName = "ViajaNet - Checkout";
            const string VendorName = "Chrome";
            const string VendorVersion = "58.34.43.2019";
            const string IP = "127.0.0.0";

            var vendor = new AnalyticsPayload.VendorPayload(VendorName, VendorVersion);

            var parameters = new Dictionary<string, List<string>>
            {
                { "bar", new List<string> { "true" } }
            };

            var payload = new AnalyticsPayload(PageName, vendor, parameters);

            //Act
            payload.AddIp(IP);

            //Assert
            Assert.NotNull(vendor);
            Assert.NotNull(payload);
            Assert.Equal(IP, payload.IP);
        }

        [Fact]
        public void Validate_Adding_IP_As_IPAddress_To_Instance_Success()
        {
            //Arrange
            const string PageName = "ViajaNet - Checkout";
            const string VendorName = "Chrome";
            const string VendorVersion = "58.34.43.2019";
            const string IP = "127.0.0.0";

            var vendor = new AnalyticsPayload.VendorPayload(VendorName, VendorVersion);

            var parameters = new Dictionary<string, List<string>>
            {
                { "bar", new List<string> { "true" } }
            };

            var payload = new AnalyticsPayload(PageName, vendor, parameters);

            //Act
            payload.AddIp(IPAddress.Parse(IP));

            //Assert
            Assert.NotNull(vendor);
            Assert.NotNull(payload);
            Assert.Equal(IP, payload.IP);
        }
    }
}
