using System.Runtime.InteropServices;

using Microsoft.Extensions.Configuration;

namespace AspNetMicroservices.Gateway.Common.Settings.RemoteServices.Implementation
{
	public class RemoteServicesSettings : IRemoteServicesSettings
	{
		public RemoteServicesSettings(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		private readonly bool _isOSXPlatform = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		private readonly IConfiguration _configuration;

		public string ProductServiceUrl => _isOSXPlatform
			? Get("Service:ProductService:HttpUrl").Value
			: Get("Service:ProductService:HttpsUrl").Value;

		private IConfigurationSection Get(string path) => _configuration.GetSection(path);
	}
}