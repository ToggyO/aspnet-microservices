using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Common.Interfaces;

using StackExchange.Redis;

namespace AspNetMicroservices.Auth.Infrastructure.Services
{
	// TODO: add description
	public class RedisClientService : ICacheService
	{
		private readonly IConnectionMultiplexer _connectionMultiplexer;

		public RedisClientService(IConnectionMultiplexer connectionMultiplexer)
		{
			_connectionMultiplexer = connectionMultiplexer;
		}

		/// <inheritdoc cref="ICacheService.GetCacheValueAsync{TValue}"/>.
		public Task<TValue> GetCacheValueAsync<TValue>(string key)
		{
			
		}

		/// <inheritdoc cref="ICacheService.SetCacheValueAsync{TValue}"/>.
		public Task SetCacheValueAsync<TValue>(string key, TValue value)
		{
			throw new System.NotImplementedException();
		}
	}
}