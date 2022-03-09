using System;
using System.Linq;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetMicroservices.Extensions.ApiVersioning.Filters
{
	// TODO: add description
	public class SwaggerRemoveVersionParameters : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var versionParameter = operation.Parameters.FirstOrDefault(
				x => string.Equals(x.Name, "version", StringComparison.OrdinalIgnoreCase));

			if (versionParameter is not null)
				operation.Parameters.Remove(versionParameter);

			var apiVersionParameter = operation.Parameters.FirstOrDefault(
				x => string.Equals(x.Name, "api-version", StringComparison.OrdinalIgnoreCase));

			if (apiVersionParameter is not null)
				operation.Parameters.Remove(apiVersionParameter);
		}
	}
}