using System;
using System.Text.Json;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace AspNetMicroservices.SharedServices.Cache.Redis
{
	/// <summary>
	/// Provides possibility to store data in Redis cache.
	/// </summary>
	public class RedisClientService : ICacheService
	{
		private const int DefaultExpirationTime = 60 * 60;

		private readonly IConnectionMultiplexer _connectionMultiplexer;

		private readonly JsonSerializerOptions _serializerOptions;

		/// <summary>
		/// Initialize new instance of <see cref="RedisClientService"/>.
		/// </summary>
		/// <param name="connectionMultiplexer">Stack exchange connection multiplexer.</param>
		public RedisClientService(IConnectionMultiplexer connectionMultiplexer)
		{
			_connectionMultiplexer = connectionMultiplexer;
			_serializerOptions = new JsonSerializerOptions();
		}

		/// <inheritdoc cref="ICacheService.GetCacheValueAsync{TValue}"/>.
		public async Task<TValue> GetCacheValueAsync<TValue>(string key)
		{
			var db = _connectionMultiplexer.GetDatabase();

			string value = await db.StringGetAsync(key);
			if (value is null)
				return default;

			return JsonSerializer.Deserialize<TValue>(value, _serializerOptions);
		}

		/// <inheritdoc cref="ICacheService.SetCacheValueAsync{TValue}(string, TValue)"/>.
		public async Task SetCacheValueAsync<TValue>(string key, TValue value)
			=> await SetCacheValueAsync(key, value, TimeSpan.FromSeconds(DefaultExpirationTime));


		/// <inheritdoc cref="ICacheService.SetCacheValueAsync{TValue}(string, TValue, TimeSpan)"/>.
		public async Task SetCacheValueAsync<TValue>(string key, TValue value, TimeSpan timeToLive)
		{
			var db = _connectionMultiplexer.GetDatabase();
			var stringValue = JsonSerializer.Serialize(value);
			await db.StringSetAsync(key, stringValue, timeToLive);
		}

		/// <inheritdoc cref="ICacheService.RemoveCacheValueAsync"/>.
		public async Task<bool> RemoveCacheValueAsync(string key)
		{
			var db = _connectionMultiplexer.GetDatabase();
			return await db.KeyDeleteAsync(key);
		}
	}
}