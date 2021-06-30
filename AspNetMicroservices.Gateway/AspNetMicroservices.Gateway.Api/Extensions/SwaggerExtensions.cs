using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AspNetMicroservices.Gateway.Api.Extensions
{
    /// <summary>
    /// Provides an extension methods for Swagger documentation package.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Extension method adds configured Swagger documentation service
        /// to instance of <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
        public static void AddConfiguredSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetMicroservices.Gateway.Api", Version = "v1" });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Extension method adds configured Swagger documentation middleware to
        /// instance of <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c
                => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetMicroservices.Gateway.Api v1"));
        }
    }
}