using AspNetMicroservices.Extensions.ApiVersioning.Filters;
using AspNetMicroservices.Extensions.Swagger.Configuration;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetMicroservices.Extensions.ApiVersioning.Configuration
{
	// TODO: add description
	/// <summary>
	///
	/// </summary>
	internal class ApiVersionedSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		private readonly SwaggerGenConfiguration _configuration;

		public ApiVersionedSwaggerOptions(IApiVersionDescriptionProvider provider,
			SwaggerGenConfiguration configuration)
		{
			_provider = provider;
			_configuration = configuration;
		}

		public void Configure(SwaggerGenOptions options)
		{
			options.DocumentFilter<SwaggerSetVersionInPaths>();
			options.OperationFilter<SwaggerRemoveVersionParameters>();

			foreach (var description in _provider.ApiVersionDescriptions)
				options.SwaggerDoc(
					description.GroupName,
					new OpenApiInfo
					{
						Title = _configuration.Title,
						Description = description.ApiVersion.ToString(),
						Version = description.GroupName
					});
		}
	}
}