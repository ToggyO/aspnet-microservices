using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetMicroservices.Extensions.Swagger.Configuration
{
	// TODO: add description
	public class ApiVersionedSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
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