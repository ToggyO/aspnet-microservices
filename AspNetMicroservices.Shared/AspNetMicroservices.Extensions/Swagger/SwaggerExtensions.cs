using System;
using System.IO;

using AspNetMicroservices.Common.Constants.Http;
using AspNetMicroservices.Extensions.ApiVersioning;
using AspNetMicroservices.Extensions.Swagger.Configuration;
using AspNetMicroservices.Extensions.Swagger.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
        /// Extension method adds configured Swagger documentation service
        /// to instance of <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
        /// <param name="configured">Swagger options retriever.</param>
        public static void AddConfiguredSwaggerGen(this IServiceCollection services,
            Action<SwaggerGenConfiguration> configured)
        // public static void AddConfiguredSwaggerGen(this IServiceCollection services,
        //     string title, string version,  bool useFullModelName = false)
        {
            var options = new SwaggerGenConfiguration();
            configured(options);

            services.AddSingleton(options);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiVersionedSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                // TODO: check
                if (options.UseFullModelName)
                    c.CustomSchemaIds(x => x.FullName);
                //

                c.DocumentFilter<SwaggerSetVersionInPaths>();
                c.OperationFilter<SwaggerRemoveVersionParameters>();

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
        /// <param name="app"></param>
        public static void UseSwaggerMiddleware(this IApplicationBuilder app,
            string name)
        {

	        // TODO: check
            // app.UseSwagger();
            // app.UseSwaggerUI(c
            //     => c.SwaggerEndpoint(swaggerEndpoint, name));

            // if (!env.IsDevelopment())
            // {
	           //  app.UseSwagger(options =>
	           //  {
		          //   options.RouteTemplate = "swagger/{documentName}/swagger.json";
		          //   options.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers = new System.Collections.Generic.List<OpenApiServer>
		          //   {
			         //    new OpenApiServer { Url = $"https://api.squad.magora.team", Description = "Dev"},
			         //    new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}", Description = "Local"}
		          //   });
	           //  });
            // }
            // else
            // {
	           //  app.UseSwagger();
            // }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
	            var apiVersionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

	            foreach (var description in apiVersionProvider.ApiVersionDescriptions)
		            options.SwaggerEndpoint($"{description.GroupName}/swagger.json"
			            , $"{name} {description.GroupName.ToUpperInvariant()}");

	            options.DocExpansion(DocExpansion.None);
            });
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