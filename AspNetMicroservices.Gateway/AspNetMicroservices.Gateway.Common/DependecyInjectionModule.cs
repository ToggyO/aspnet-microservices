using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Gateway.Common
{
	public static class DependencyInjectionModule
	{
		public static void Load(IServiceCollection services,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
		}
	}
}