using System.Reflection;

namespace AspNetMicroservices.Extensions.Swagger.Configuration
{
	// TODO: add description
	public class SwaggerGenConfiguration
	{
		public string Title { get; set; }

		// public string Version { get; set; }

		public bool UseFullModelName { get; set; }

		public Assembly ExecutingAssembly { get; set; }
	}
}