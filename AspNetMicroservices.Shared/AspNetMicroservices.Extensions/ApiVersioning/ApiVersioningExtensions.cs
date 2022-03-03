using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Extensions.ApiVersioning
{
	// TODO: add description
	public static class ApiVersioningExtensions
	{
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

		// public static IServiceCollection AddConfiguredVersionedSwaggerGen(this IServiceCollection services)
		// {
		//
		// }
	}
}