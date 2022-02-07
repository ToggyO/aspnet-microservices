using System.Runtime.InteropServices;

using Microsoft.Extensions.Configuration;

namespace AspNetMicroservices.Gateway.Common.Settings.RemoteServices
{
	/// <summary>
	/// Remote services urls.
	/// </summary>
	public class RemoteServicesSettings
	{
		/// <summary>
		/// Creates an instance of <see cref="RemoteServicesSettings"/>.
		/// </summary>
		/// <param name="configuration">Instance of <see cref="IConfiguration"/>.</param>
		public RemoteServicesSettings(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// If application running on OSX platform.
		/// </summary>
		private readonly bool _isOSXPlatform = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

		/// <summary>
		/// Instance of <see cref="IConfiguration"/>.
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Product service url.
		/// </summary>
		public string ProductServiceUrl => _isOSXPlatform
			? Get("Services:ProductService:HttpUrl")
			: Get("Services:ProductService:HttpsUrl");

		/// <summary>
		/// Gets a value from an instance of <see cref="IConfiguration"/>.
		/// </summary>
		/// <param name="path">Path to value from configuration (can be nested).</param>
		/// <returns></returns>
		private string Get(string path) => _configuration[path];
	}
}