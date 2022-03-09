using System;

using AspNetMicroservices.Extensions.ApiVersioning.Configuration;
using AspNetMicroservices.Extensions.Swagger;
using AspNetMicroservices.Extensions.Swagger.Configuration;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AspNetMicroservices.Extensions.ApiVersioning
{
	/// <summary>
	/// Provides an extension methods for Api Versioning package.
	/// </summary>
	public static class ApiVersioningExtensions
	{
		/// <summary>
		/// Extension method adds configured api versioning services.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <returns></returns>
		public static IServiceCollection AddConfiguredApiVersioning(this IServiceCollection services)
		{
			services.AddApiVersioning(o =>
			{
				o.ReportApiVersions = true;
				o.AssumeDefaultVersionWhenUnspecified = true;
				o.DefaultApiVersion = ApiVersion.Default;
				o.ApiVersionReader = new UrlSegmentApiVersionReader();
			});

			services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");

			return services;
		}

		/// <summary>
		/// Extension method adds configured Swagger documentation service in
		/// accordance to api versioning services.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="configured">Configure swagger documentation options action.</param>
		/// <returns></returns>
		public static IServiceCollection AddConfiguredVersionedSwaggerGen(this IServiceCollection services,
			Action<SwaggerGenConfiguration> configured)
		{
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiVersionedSwaggerOptions>();
			services.AddTransient<IConfigureOptions<SwaggerUIOptions>, ApiVersionedSwaggerUIOptions>();

			services.AddConfiguredSwaggerGen(configured);

			return services;
		}
	}
}