using System;
using System.IO;

using AspNetMicroservices.Common.Constants.Http;
using AspNetMicroservices.Extensions.Swagger.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AspNetMicroservices.Extensions.Swagger
{
    /// <summary>
    /// Provides an extension methods for Swagger documentation package.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Extension method adds configured Swagger documentation service.
        /// to instance of <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
        /// <param name="configured">Swagger options retriever.</param>
        public static void AddConfiguredSwaggerGen(this IServiceCollection services,
            Action<SwaggerGenConfiguration> configured)
        {
            var options = new SwaggerGenConfiguration();
            configured(options);

            services.AddSingleton(options);
            services.AddSwaggerGen(c =>
            {
                // TODO: check
                if (options.UseFullModelName)
                    c.CustomSchemaIds(x => x.FullName);
                //

                c.AddJwtSecurityDefinition();

                if (options.ExecutingAssembly is not null)
                {
                    var fileName = $"{options.ExecutingAssembly.GetName().Name}.xml";
                    var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                    c.IncludeXmlComments(filePath);
                }
            });
        }

        /// <summary>
        /// Extension method adds configured Swagger documentation middleware to
        /// instance of <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app">Instance of <see cref="IApplicationBuilder"/>.</param>
        /// <param name="setupAction">Configure swagger UI options action.</param>
        /// <param name="apiServers">List of api servers.</param>
        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app,
	        Action<SwaggerUIOptions> setupAction = null,
	        System.Collections.Generic.IEnumerable<OpenApiServer> apiServers = null)
        {
	        app.UseSwagger(options =>
            {
	            options.RouteTemplate = "swagger/{documentName}/swagger.json";
	            options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
	            {
		            var servers = new System.Collections.Generic.List<OpenApiServer>
		            {
			            new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}", Description = "Local" }
		            };

		            if (apiServers is not null)
						servers.AddRange(apiServers);

		            swaggerDoc.Servers = servers;
	            });
            });
            app.UseSwaggerUICustom(setupAction);

            return app;
        }

        /// <summary>
        /// Enable usage of swagger UI middleware.
        /// </summary>
        /// <param name="app">Instance of <see cref="IApplicationBuilder"/>.</param>
        /// <param name="setupAction">Configure swagger UI options action.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerUICustom(this IApplicationBuilder app,
	        Action<SwaggerUIOptions> setupAction = null)
        {
	        var options = app.ApplicationServices
		        .GetRequiredService<IOptions<SwaggerUIOptions>>().Value ?? new SwaggerUIOptions();

	        if (setupAction is not null)
		        setupAction(options);

	        app.UseMiddleware<SwaggerUIMiddleware>(options);

	        return app;
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
}