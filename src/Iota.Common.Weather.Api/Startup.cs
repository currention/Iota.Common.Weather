using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Iota.Common.Weather.Core.Configuration;
using Iota.Common.Weather.Services;

namespace Iota.Common.Weather.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            ConfigureSettings(services);

            // Added - used for dependency injection
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped<IWeatherService, WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }


        public void ConfigureSettings(IServiceCollection services)
        {
            services.AddOptions();

            // Added - Confirms that we have a home for our DemoSettings
            services.Configure<WeatherSettings>(Configuration.GetSection("Weather"));

            var weatherSettings = new WeatherSettings();
            Configuration.GetSection("Weather").Bind(weatherSettings);
            services.AddScoped(c => weatherSettings);
        }
    }
}
