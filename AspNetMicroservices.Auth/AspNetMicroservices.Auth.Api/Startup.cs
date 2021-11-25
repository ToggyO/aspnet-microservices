using System;

using AspNetMicroservices.Auth.Api.Extensions;
using AspNetMicroservices.Auth.Api.Filters;
using AspNetMicroservices.Auth.Api.Middleware;
using AspNetMicroservices.Auth.DataAccess;
using AspNetMicroservices.Auth.Infrastructure;
using AspNetMicroservices.Shared.Models.Settings;

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
	        bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            services.AddCors(o =>
                o.AddPolicy(CorsPolicy, builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                ));

            services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);
            services.Configure<RedisSettings>(o =>
            {
	            o.Host = DotNetEnv.Env.GetString("AUTH_REDIS_HOST");
	            o.Db = DotNetEnv.Env.GetInt("AUTH_REDIS_DATABASE");
	            o.Password = DotNetEnv.Env.GetString("AUTH_REDIS_PASSWORD");
	            o.Port = DotNetEnv.Env.GetInt(isDevelopment ? "AUTH_REDIS_EXTERNAL_PORT" : "AUTH_REDIS_PORT");
	            o.KeyExpirationInSec = DotNetEnv.Env.GetInt("AUTH_REDIS_KEY_EXPIRATION_IN_SEC");
            });

            var dbSettings = new PostgresDataSettings
            {
	            Host = DotNetEnv.Env.GetString("AUTH_DB_HOST"),
	            Port = DotNetEnv.Env.GetInt(isDevelopment ? "AUTH_DB_EXTERNAL_PORT" : "AUTH_DB_PORT"),
	            User = DotNetEnv.Env.GetString("AUTH_DB_USER"),
	            Password = DotNetEnv.Env.GetString("AUTH_DBPASSWORD"),
	            DbName = DotNetEnv.Env.GetString("AUTH_DB_NAME"),
            };

            services.AddDataAccess(dbSettings.DbConnectionString);
            services.AddInfrastructure();

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
