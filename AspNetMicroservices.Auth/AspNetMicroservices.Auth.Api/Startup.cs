using AspNetMicroservices.Auth.Api.Extensions;
using AspNetMicroservices.Auth.Api.Filters;
using AspNetMicroservices.Auth.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetMicroservices.Auth.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            DotNetEnv.Env.TraversePath().Load();
        }

        /// <summary>
        /// Gets an instance of <see cref="IConfiguration"/>
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Cors policy identifier
        /// </summary>
        private const string CorsPolicy = "AspNetMicroservicesCorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
                o.AddPolicy(CorsPolicy, builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                ));

            services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);

            services.AddControllersWithViews(mvcOpts =>
            {
                mvcOpts.UseGlobalRoutePrefix("api");
                mvcOpts.Filters.Add<StatusCodeFilter>();
            });
            services.AddConfiguredSwaggerGen();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseSwaggerMiddleware();

            logger.LogInformation("Routing");
            app.UseRouting();

            logger.LogInformation("Cors");
            app.UseCors(CorsPolicy);

            logger.LogInformation("Custom middleware");
            app.UseMiddleware<ExceptionMiddleware>();

            logger.LogInformation("Endpoints");
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            logger.LogInformation("Exit Configure");
        }
    }
}
