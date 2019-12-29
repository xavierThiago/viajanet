using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ViajaNet.JobApplication.Host.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(options =>
                {
                    options.ClearProviders();
                    options.AddDebug();
                    options.AddConsole();
                })
                .UseKestrel(options =>
                {
                    options.ListenAnyIP(5000, server =>
                    {
                        server.Protocols = HttpProtocols.Http1AndHttp2;
                    });

                    // TODO: implement HTTPS on Docker container.
                    /* options.ListenAnyIP(5001, server =>
                    {
                        server.Protocols = HttpProtocols.Http1AndHttp2;

                        server.UseHttps();
                    }); */
                })
                // TODO: implement HTTPS on Docker container.
                .UseUrls("http://+:5000")
                .UseStartup<Startup>();
    }
}
