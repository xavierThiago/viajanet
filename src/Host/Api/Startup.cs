using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace ViajaNet.JobApplication.Host.Api
{
    public class Startup
    {
        private const string LocalHtmlFilePolicy = "LocalHtmlFilePolicy";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging()
                .AddCors(options =>
                {
                    // When requests are made from file protocol, in some browsers, the origin is 'null'.
                    const string LocalFileOrigin = "null";

                    options.AddPolicy(LocalHtmlFilePolicy, x =>
                    {
                        x.WithOrigins(LocalFileOrigin)
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .Build();
                    });
                })
                .AddOptions()
                .AddResponseCaching()
                .AddResponseCompression()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "ViajaNet Analytics API",
                    Version = "v1",
                    Description = "Simple analytics collector API.",
                    TermsOfService = "https://github.com/xavierThiago/viajanet",
                    Contact = new Contact
                    {
                        Name = "Thiago J. Xavier",
                        Email = "xavier.j.thiago@gmail.com"
                    },
                    License = new License
                    {
                        Name = "Apache 2.0",
                        Url = "http://www.apache.org/licenses/LICENSE-2.0.html"
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseCors(LocalHtmlFilePolicy);
            }
            else
            {
                app.UseHsts();
            }

            app.UseResponseCompression()
                .UseResponseCaching()
                .UseAuthentication()
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ViajaNet Analytics API");
                })
                .UseMvc();
        }
    }
}
