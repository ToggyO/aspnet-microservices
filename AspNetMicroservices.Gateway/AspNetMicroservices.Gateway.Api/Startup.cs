using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using AspNetMicroservices.Gateway.Api.Extensions;
using AspNetMicroservices.Gateway.Api.Filters;
using AspNetMicroservices.Gateway.Api.Middleware;
using AspNetMicroservices.Gateway.Common.Settings.RemoteServices;
using AspNetMicroservices.Shared.Protos;

namespace AspNetMicroservices.Gateway.Api
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
            services.AddCors(options =>
                options.AddPolicy(CorsPolicy,
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    ));

            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

            var remoteServicesSettings = new RemoteServicesSettings(Configuration);
            services.AddConfiguredGrpcClient<ProductsService.ProductsServiceClient>(remoteServicesSettings.ProductServiceUrl);
            // services.AddConfiguredGrpcClient<ProductsService.ProductsServiceClient>("https://localhost:5003");
                // .AddInterceptor<RpcErrorInterceptor>();

            services.AddControllersWithViews(opt =>
            {
	            opt.UseGlobalRoutePrefix("api");
	            opt.Filters.Add<StatusCodeFilter>();
            });
            services.AddConfiguredSwaggerGen();

            DependencyInjectionModule.LoadRpcDependencies(services);
            DependencyInjectionModule.Load(services);
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger)
        {
            logger.LogInformation("Enter Configure");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerMiddleware();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            logger.LogInformation("Routing");
            app.UseRouting();

            logger.LogInformation("Cors");
            app.UseCors(CorsPolicy);

            // logger.LogInformation("Auth middleware");
            // app.UseAuthorization();

            logger.LogInformation("Custom middleware");
            app.UseMiddleware<ExceptionMiddleware>();

            logger.LogInformation("Endpoints");
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            logger.LogInformation("Exit Configure");
        }
    }
}
