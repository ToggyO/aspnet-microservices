using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace AspNetMicroservices.Extensions.ApiVersioning.Configuration
{
	/// <summary>
	/// Represents swagger UI options configuration.
	/// </summary>
	public class ApiVersionedSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
	{
		/// <summary>
		/// Versioning provider.
		/// </summary>
		private readonly IApiVersionDescriptionProvider _provider;

		/// <summary>
		/// Initialize new instance of <see cref="ApiVersionDescription"/>.
		/// </summary>
		/// <param name="provider">Instance of <see cref="IApiDescriptionProvider"/>.</param>
		public ApiVersionedSwaggerUIOptions(IApiVersionDescriptionProvider provider)
		{
			_provider = provider;
		}

		/// <inheritdoc cref="IConfigureOptions{TOptions}.Configure"/>.
		public void Configure(SwaggerUIOptions options)
		{
			foreach (var description in _provider.ApiVersionDescriptions)
				options.SwaggerEndpoint($"{description.GroupName}/swagger.json"
					, description.GroupName.ToUpperInvariant());
		}
	}
}