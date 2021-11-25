using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Infrastructure.Services;
using AspNetMicroservices.Shared.Extensions;
using AspNetMicroservices.Shared.Models.Settings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using StackExchange.Redis;

namespace AspNetMicroservices.Auth.Infrastructure
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add infrastructure module to an application.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="serviceLifetime">Service lifetime.</param>
		public static void AddInfrastructure(this IServiceCollection services,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			services.AddSingleton<IConnectionMultiplexer>(x =>
			{
				using var scope = x.CreateScope();
				var redisOptions = scope.ServiceProvider.GetRequiredService<IOptions<RedisSettings>>().Value;
				return ConnectionMultiplexer.Connect(redisOptions.ConnectionString);
			});

			services.Add<ICacheService, RedisClientService>(serviceLifetime);
		}
	}
}