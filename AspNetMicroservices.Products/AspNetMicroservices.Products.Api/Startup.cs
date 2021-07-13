using System;
using System.Reflection;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.Configuration;

using AspNetMicroservices.Products.Api.Services;
using AspNetMicroservices.Products.Business.Behaviours;
using AspNetMicroservices.Products.Common.Settings;
using AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection;
using AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection.Implementation;

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
            services.AddGrpc(opts =>
            {
                opts.EnableDetailedErrors = true;
            });

            services.AddMediatR(typeof(Business.DependencyInjectionModule));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(Business.DependencyInjectionModule).Assembly);

            var dbSettings = new DbSettings
            {
                DB_HOST = DotNetEnv.Env.GetString("POSTGRES_DB_HOST"),
                DB_PORT = DotNetEnv.Env.GetString(
                    isDevelopment ? "PRODUCTS_DB_EXTERNAL_PORT" : "PRODUCTS_DB_PORT"),
                DB_NAME = DotNetEnv.Env.GetString("PRODUCTS_DB_NAME"),
                DB_USER = DotNetEnv.Env.GetString("PRODUCTS_DB_USER"),
                DB_PASSWORD = DotNetEnv.Env.GetString("PRODUCTS_DB_PASSWORD"),
            };
            services.AddLinqToDbContext<IAppDataConnection, AppDataConnection>((provider, options) =>
            {
                options.UsePostgreSQL(dbSettings.DbConnectionString);
            });
            services.AddFluentMigratorCore()
	            .ConfigureRunner(cfg => cfg
		            .AddPostgres()
		            .WithGlobalConnectionString(dbSettings.DbConnectionString)
		            .ScanIn(Assembly.GetAssembly(typeof(AppDataConnection))).For.All()
	            )
	            .AddLogging(cfg => cfg.AddFluentMigratorConsole());

            services.AddAutoMapper(typeof(Business.DependencyInjectionModule));
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
