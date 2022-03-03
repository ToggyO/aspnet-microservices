using System;
using System.Reflection;

using AspNetMicroservices.Abstractions.Models.Settings;
using AspNetMicroservices.Auth.Api.Filters;
using AspNetMicroservices.Auth.Api.Middleware;
using AspNetMicroservices.Auth.Application;
using AspNetMicroservices.Auth.DataAccess;
using AspNetMicroservices.Auth.Infrastructure;
using AspNetMicroservices.Common.Utils;
using AspNetMicroservices.Extensions.ApiVersioning;
using AspNetMicroservices.Extensions.Mvc;
using AspNetMicroservices.Extensions.Swagger;
using AspNetMicroservices.Logging.Serilog;
using AspNetMicroservices.SharedServices.Cache;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
	        bool isDevelopment = Environment
		        .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            services.AddCors(o =>
                o.AddPolicy(CorsPolicy, builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                ));

            var dbSettings = new PostgresDataSettings
            {
	            Host = DotNetEnv.Env.GetString("AUTH_DB_HOST"),
	            Port = DotNetEnv.Env.GetInt(isDevelopment ? "AUTH_DB_EXTERNAL_PORT" : "AUTH_DB_PORT"),
	            User = DotNetEnv.Env.GetString("AUTH_DB_USER"),
	            Password = DotNetEnv.Env.GetString("AUTH_DB_PASSWORD"),
	            DbName = DotNetEnv.Env.GetString("AUTH_DB_NAME"),
            };

            var redisSettings = new RedisSettings
            {
	            Host = DotNetEnv.Env.GetString("AUTH_REDIS_HOST"),
	            Db = DotNetEnv.Env.GetInt("AUTH_REDIS_DATABASE"),
	            Password = DotNetEnv.Env.GetString("AUTH_REDIS_PASSWORD"),
	            Port = DotNetEnv.Env.GetInt(isDevelopment ? "AUTH_REDIS_EXTERNAL_PORT" : "PRODUCTS_REDIS_PORT"),
	            KeyExpirationInSec = DotNetEnv.Env.GetInt("AUTH_REDIS_KEY_EXPIRATION_IN_SEC")
            };

            services.AddSettings(Configuration, isDevelopment);
            services.AddApplicationLayer();
            services.AddInfrastructure(Configuration);
            services.AddDataAccess(dbSettings.DbConnectionString);
            services.AddRedisCache(redisSettings.ConnectionString);
            services.AddApiHandlers();
            // TODO: check
            // services.AddAuthServices(Configuration);

            services.AddControllersWithViews(mvcOpts =>
            {
	            mvcOpts.UseGlobalRoutePrefix("api/{version:apiVersion}");
	            mvcOpts.Filters.Add<ApplicationExceptionFilterAttribute>();
	            mvcOpts.Filters.Add<AuthorizationFilter>();
                // mvcOpts.Filters.Add<ValidationFilter>();
	            mvcOpts.Filters.Add<StatusCodeFilter>();
            });

            services.AddConfiguredApiVersioning();
            services.AddConfiguredSwaggerGen(o =>
            {
                o.Title = "AspNetMicroservices.Auth.Api";
                // o.Version = "v1";
                o.UseFullModelName = true;
                o.ExecutingAssembly = Assembly.GetExecutingAssembly();
            });
        }

        public void Configure(IApplicationBuilder app,
	        IHostApplicationLifetime hostLifetime,
            IWebHostEnvironment env,
            ILogger<Startup> logger)
        {
	        hostLifetime.UseSerilogCloseAndFlush();

	        logger.LogInformation("Startup.Configure: Start");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            logger.LogInformation("Startup.Configure: Api documentation");
            app.UseSwaggerMiddleware(Project.GetCurrentSolutionName());

            logger.LogInformation("Startup.Configure: Routing");
            app.UseRouting();

            logger.LogInformation("Startup.Configure: Cors");
            app.UseCors(CorsPolicy);

            // TODO: check
            // logger.LogInformation("Startup.Configure: Authorization");
            // app.UseAuthentication();
            // app.UseAuthorization();

            logger.LogInformation("Startup.Configure: Custom middleware");
            app.UseMiddleware<ExceptionMiddleware>();

            logger.LogInformation("Startup.Configure: Endpoints");
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            logger.LogInformation("Startup.Configure: Finish");
        }
    }
}
