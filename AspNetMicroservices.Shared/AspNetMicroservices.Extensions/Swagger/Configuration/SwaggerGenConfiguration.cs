using System.Reflection;

namespace AspNetMicroservices.Extensions.Swagger.Configuration
{
	/// <summary>
	/// Represents swagger services configuration options.
	/// </summary>
	public class SwaggerGenConfiguration
	{
		/// <summary>
		/// The title of the application.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Indicates whether to user full type names.
		/// </summary>
		public bool UseFullModelName { get; set; }

		/// <summary>
		/// Current executing assembly.
		/// </summary>
		public Assembly ExecutingAssembly { get; set; }
	}
}