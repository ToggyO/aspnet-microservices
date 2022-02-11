using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Common.Constants.Claims;
using AspNetMicroservices.Common.Constants.Common;
using AspNetMicroservices.Grpc.Exceptions;
using AspNetMicroservices.Products.Dto.Users;
using AspNetMicroservices.SharedServices.Cache;

using Grpc.Core;
using Grpc.Core.Interceptors;

namespace AspNetMicroservices.Products.Api.Interceptors
{
	/// <summary>
	/// Authentication interceptor for Grpc services.
	/// </summary>
	public class AuthInterceptor : Interceptor
	{
		/// <summary>
		/// Application cache service.
		/// </summary>
		private readonly ICacheService _cache;

		/// <summary>
		/// Initialize new instance of <see cref="AuthInterceptor"/>.
		/// </summary>
		/// <param name="cache">Cache service.</param>
		public AuthInterceptor(ICacheService cache)
		{
			_cache = cache;
		}

		public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
			TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
		{
			var httpContext = context.GetHttpContext();
			var claimsPrincipal = httpContext.User;

			var identityId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.IdentityId)?.Value;
			var ticket = await _cache.GetCacheValueAsync<AuthenticationTicket<User>>(
				$"{Prefix.Access}::{identityId}");

			if (ticket is null)
				throw new ErrorResponseRpcException(StatusCode.Unauthenticated, new SecurityErrorResponse());

			return await base.UnaryServerHandler(request, context, continuation);
		}
	}
}