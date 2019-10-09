using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Serilog.ServiceTwo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITelemetryInitializer, Models.AppInsightsTelemetryInitializer>();
            services.AddApplicationInsightsTelemetry();
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
            {
                module.Initialize(TelemetryConfiguration.Active);
                module.EnableW3CHeadersInjection = true;
            });

            var sp = services.BuildServiceProvider();
            var telemetryConfig = sp.GetRequiredService<TelemetryConfiguration>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ApplicationInsights(telemetryConfig, TelemetryConverter.Traces)
                .CreateLogger();

            services.AddSingleton<Serilog.ILogger>(Log.Logger);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
