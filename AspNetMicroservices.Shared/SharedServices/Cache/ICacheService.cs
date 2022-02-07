using System;
using System.Threading.Tasks;

namespace AspNetMicroservices.Shared.SharedServices.Cache
{
	/// <summary>
	/// Represents cache service.
	/// </summary>
	public interface ICacheService
	{
		/// <summary>
		/// Gets value from cache by specified key and transforms to provided type <see cref="TValue"/>.
		/// </summary>
		/// <param name="key">The key by which the data will be searched in cache storage.</param>
		/// <typeparam name="TValue">Received value type.</typeparam>
		/// <returns></returns>
		Task<TValue> GetCacheValueAsync<TValue>(string key);

		/// <summary>
		/// Sets value to cache by specified key.
		/// </summary>
		/// <param name="key">The key associated with the stored value.</param>
		/// <param name="value">Value to be stored in cache.</param>
		/// <typeparam name="TValue">Value type.</typeparam>
		/// <returns></returns>
		Task SetCacheValueAsync<TValue>(string key, TValue value);

		/// <summary>
		/// Sets value to cache by specified key with expiration time.
		/// </summary>
		/// <inheritdoc cref="SetCacheValueAsync{TValue}(string,TValue)"/>
		/// <param name="timeToLive">Expiration time.</param>
		/// <returns></returns>
		Task SetCacheValueAsync<TValue>(string key, TValue value, TimeSpan timeToLive);

		/// <summary>
		/// Removes value from cache by specified key.
		/// </summary>
		/// <param name="key">The key associated with the stored value.</param>
		/// <returns></returns>
		Task<bool> RemoveCacheValueAsync(string key);
	}
}