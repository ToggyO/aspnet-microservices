using System;
using System.Text.Json;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Shared.Models.Settings;

using Microsoft.Extensions.Options;

using StackExchange.Redis;

namespace AspNetMicroservices.Auth.Infrastructure.Services
{
	/// <summary>
	/// Provides possibility to store data in Redis cache.
	/// </summary>
	public class RedisClientService : ICacheService
	{
		private readonly IOptions<RedisSettings> _redisOptions;

		private readonly IConnectionMultiplexer _connectionMultiplexer;

		private readonly JsonSerializerOptions _serializerOptions;

		/// <summary>
		/// Initialize new instance of <see cref="RedisClientService"/>.
		/// </summary>
		/// <param name="connectionMultiplexer">Stack exchange connection multiplexer.</param>
		/// <param name="redisOptions">Redis connection options.</param>
		public RedisClientService(IConnectionMultiplexer connectionMultiplexer,
			IOptions<RedisSettings> redisOptions)
		{
			_connectionMultiplexer = connectionMultiplexer;
			_redisOptions = redisOptions;
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
			=> await SetCacheValueAsync(key, value, TimeSpan.FromSeconds(_redisOptions.Value.KeyExpirationInSec));


		/// <inheritdoc cref="ICacheService.SetCacheValueAsync{TValue}(string, TValue, TimeSpan)"/>.
		public async Task SetCacheValueAsync<TValue>(string key, TValue value, TimeSpan timeToLive)
		{
			var db = _connectionMultiplexer.GetDatabase();
			var stringValue = JsonSerializer.Serialize(value);
			await db.StringSetAsync(key, stringValue, timeToLive);
		}
	}
}