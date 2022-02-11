using AspNetMicroservices.Extensions.ServiceCollection;
using AspNetMicroservices.SharedServices.Cache.Redis;

using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace AspNetMicroservices.SharedServices.Cache
{
	/// <summary>
	/// Cache services dependency injection module.
	/// </summary>
	public static class DependencyInjection
	{
		/// <summary>
		/// Add Redis cache service to an application services.
		/// </summary>
		/// <param name="services">Application services.</param>
		/// <param name="connectionString">Connection string to a redis instance.</param>
		/// <param name="serviceLifetime">Service lifetime.</param>
		public static void AddRedisCache(this IServiceCollection services,
			string connectionString,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			services.AddSingleton<IConnectionMultiplexer>(x
				=> ConnectionMultiplexer.Connect(connectionString));

			services.Add<ICacheService, RedisClientService>(serviceLifetime);
		}
	}
}