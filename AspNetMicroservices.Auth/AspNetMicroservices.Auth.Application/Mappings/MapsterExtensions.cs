using System.Reflection;

using AspNetMicroservices.Shared.Extensions;

using Mapster;

using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Auth.Application.Mappings
{
	/// <summary>
	/// Extensions for MapsterMapper <see cref="https://github.com/MapsterMapper/Mapster"/>.
	/// </summary>
	public static class MapsterExtensions
	{
		/// <summary>
		/// Add MapsterMapper services to an application services.
		/// </summary>
		/// <param name="services"></param>
		public static void AddMapsterMapper(this IServiceCollection services,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			var config = GetMapsterConfig();

			services.AddSingleton(config.GetType(), config);
			services.Add<IMapper, ServiceMapper>(serviceLifetime);

			config.Scan(Assembly.GetExecutingAssembly());
		}

		private static TypeAdapterConfig GetMapsterConfig()
		{
			var config = new TypeAdapterConfig();

			config.AllowImplicitDestinationInheritance = true;
			config.AllowImplicitSourceInheritance = true;

			return config;
		}
	}
}