using System;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Shared.SharedServices.Cache;

using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Auth.Api.Controllers.v1
{
	[ApiController]
	[Route("cache")]
	public class CacheController : ControllerBase
	{
		private readonly ICacheService _cache;

		public CacheController(ICacheService cache)
		{
			_cache = cache;
		}

		[HttpGet("{key}")]
		public async Task<string> Get([FromRoute] string key)
		{
			var value = await _cache.GetCacheValueAsync<UserModel>(key);
			if (value is null)
				return String.Empty;
			return value.FirstName;
		}

		[HttpPost("{key}")]
		public async Task Create([FromRoute] string key)
		{
			var user = new UserModel
			{
				Email = "sa@sd.com",
				FirstName = "Slava",
				LastName = "Ukrainin",
			};
			await _cache.SetCacheValueAsync(key, user);
			var value = await _cache.GetCacheValueAsync<UserModel>(key);
			Console.WriteLine(value.FirstName);
		}
	}
}