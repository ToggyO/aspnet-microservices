using System;
using System.Reflection;

using AspNetMicroservices.Abstractions.Models.Settings;
using AspNetMicroservices.Products.Api.Interceptors;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using FluentMigrator.Runner;

using AspNetMicroservices.Products.Api.Services;
using AspNetMicroservices.Products.Business;
using AspNetMicroservices.Products.DataLayer;
using AspNetMicroservices.SharedServices.Cache;

namespace AspNetMicroservices.Products.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            DotNetEnv.Env.TraversePath().Load();
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            var dbSettings = new PostgresDataSettings
            {
	            Host = DotNetEnv.Env.GetString("POSTGRES_DB_HOST"),
	            Port = DotNetEnv.Env.GetInt(
		            isDevelopment ? "PRODUCTS_DB_EXTERNAL_PORT" : "PRODUCTS_DB_PORT"),
	            DbName = DotNetEnv.Env.GetString("PRODUCTS_DB_NAME"),
	            User = DotNetEnv.Env.GetString("PRODUCTS_DB_USER"),
	            Password = DotNetEnv.Env.GetString("PRODUCTS_DB_PASSWORD"),
            };

            var redisSettings = new RedisSettings
            {
	            Host = DotNetEnv.Env.GetString("PRODUCTS_REDIS_HOST"),
	            Db = DotNetEnv.Env.GetInt("PRODUCTS_REDIS_DATABASE"),
	            Password = DotNetEnv.Env.GetString("PRODUCTS_REDIS_PASSWORD"),
	            Port = DotNetEnv.Env.GetInt(
		            isDevelopment ? "PRODUCTS_REDIS_EXTERNAL_PORT" : "PRODUCTS_REDIS_PORT"),
	            KeyExpirationInSec = DotNetEnv.Env.GetInt("PRODUCTS_REDIS_KEY_EXPIRATION_IN_SEC")
            };

            services.AddGrpc(opts =>
            {
	            opts.EnableDetailedErrors = true;
	            opts.Interceptors.Add<AuthInterceptor>();
            });

            services.AddSettings(Configuration);
            services.AddBusinessLayer();
            services.AddDataLayer(dbSettings.DbConnectionString);
            services.AddAuthServices(Configuration);
            services.AddRedisCache(redisSettings.ConnectionString);

            services.AddAutoMapper(typeof(Startup), typeof(Business.DependencyInjectionModule));
        }

        public void Configure(
	        IApplicationBuilder app,
	        IWebHostEnvironment env,
	        IMigrationRunner runner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProductsService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

            runner.MigrateUp();
        }
    }
}
