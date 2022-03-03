using System;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetMicroservices.Extensions.Swagger.Filters
{
	// TODO: add description
	public class SwaggerSetVersionInPaths : IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			var openApiPaths = new OpenApiPaths();
			foreach (var (key, value) in swaggerDoc.Paths)
			{
				openApiPaths.Add(
					key.Replace("{version}", swaggerDoc.Info.Version), value);
			}

			swaggerDoc.Paths = openApiPaths;
		}
	}
}