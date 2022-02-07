using System;
using System.IO;
using System.Reflection;

using AspNetMicroservices.Shared.Constants.Http;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetMicroservices.Shared.Extensions.Swagger
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
        public static void AddConfiguredSwaggerGen(this IServiceCollection services,
            Action<ConfiguredSwaggerGenOptions> configured)
        // public static void AddConfiguredSwaggerGen(this IServiceCollection services,
        //     string title, string version,  bool useFullModelName = false)
        {
            var options = new ConfiguredSwaggerGenOptions();
            configured(options);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = options.Title, Version = options.Version });

                // TODO: check
                if (options.UseFullModelName)
                    c.CustomSchemaIds(x => x.FullName);
                //

                c.AddJwtSecurityDefinition();

                if (options.ExcutingAssembly is not null)
                {
                    var fileName = $"{options.ExcutingAssembly.GetName().Name}.xml";
                    var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                    c.IncludeXmlComments(filePath);
                }
            });
        }

        /// <summary>
        /// Extension method adds configured Swagger documentation middleware to
        /// instance of <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerMiddleware(this IApplicationBuilder app,
            string name, string swaggerEndpoint = "/swagger/v1/swagger.json")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c
                => c.SwaggerEndpoint(swaggerEndpoint, name));
        }

        private static void AddJwtSecurityDefinition(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Name = HttpHeaderNames.Authorization,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description =
                    "Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\", provide value: \"Bearer {token}\"",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }
    }

    public class ConfiguredSwaggerGenOptions
    {
        public string Title { get; set; }

        public string Version { get; set; }

        public bool UseFullModelName { get; set; }

        public Assembly ExcutingAssembly { get; set; }
    }
}