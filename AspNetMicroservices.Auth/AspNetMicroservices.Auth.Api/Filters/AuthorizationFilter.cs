using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Common.Enums;
using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Shared.Constants.Errors;
using AspNetMicroservices.Shared.Models.Auth;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.SharedServices.Cache;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

using ClaimTypes = AspNetMicroservices.Shared.Constants.Claims.ClaimTypes;

namespace AspNetMicroservices.Auth.Api.Filters
{
	/// <summary>
	/// Represents authentication http filter.
	/// </summary>
	public class AuthorizationFilter : IAsyncAuthorizationFilter
	{
		/// <summary>
		/// Bearer authentication scheme identifier.
		/// </summary>
		private const string Bearer = "Bearer ";

		/// <summary>
		/// Tokens factory.
		/// </summary>
		private readonly ITokensFactory _tokensFactory;

		/// <summary>
		/// Application cache service.
		/// </summary>
		private readonly ICacheService _cache;

		/// <summary>
		/// Initialize new instance of <see cref="ITokensFactory"/>.
		/// </summary>
		/// <param name="tokensFactory">Instance of <see cref="ITokensFactory"/>.</param>
		/// <param name="cache">Instance of <see cref="ICacheService"/>.</param>
		public AuthorizationFilter(ITokensFactory tokensFactory, ICacheService cache)
		{
			_tokensFactory = tokensFactory;
			_cache = cache;
		}

		/// <inheritdoc cref="IAsyncAuthorizationFilter.OnAuthorizationAsync"/>.
		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
			{
				var attributes = descriptor.MethodInfo.CustomAttributes;
				if (attributes.Any(x => x.AttributeType == typeof(AllowAnonymousAttribute)))
					return;
			}

			var tokenStatus = TokenStatus.Invalid;
			string authHeader = context.HttpContext.Request.Headers["Authorization"];

			// TODO: check on refactoring possibilities.
			if (!string.IsNullOrEmpty(authHeader))
			{
				var token = GetCleanToken(authHeader);
				tokenStatus = _tokensFactory.ValidateToken(token, out ClaimsPrincipal principal);

				if (tokenStatus == TokenStatus.Valid)
				{
					string identityId = principal.Claims
						.FirstOrDefault(x => x.Type.Equals(ClaimTypes.IdentityId, StringComparison.Ordinal))?.Value;
					var ticket = await _cache.GetCacheValueAsync<AuthenticationTicket<UserDto>>(identityId);

					if (ticket is not null)
						return;
				}
			}

			var error = GetErrorByTokenStatus(tokenStatus);
			context.Result = new ObjectResult(error)
			{
				StatusCode = (int)error.HttpStatusCode,
			};
		}

		// TODO: check
		// public void OnAuthorization(AuthorizationFilterContext context)
		// {
		// 	if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
		// 	{
		// 		var attributes = descriptor.MethodInfo.CustomAttributes;
		// 		if (attributes.Any(x => x.AttributeType == typeof(AllowAnonymousAttribute)))
		// 			return;
		// 	}
		//
		// 	var tokenStatus = TokenStatus.Invalid;
		// 	string authHeader = context.HttpContext.Request.Headers["Authorization"];
		//
		// 	if (!string.IsNullOrEmpty(authHeader))
		// 	{
		// 		var token = GetCleanToken(authHeader);
		// 		tokenStatus = _tokensFactory.ValidateToken(token, out _);
		//
		// 		if (tokenStatus == TokenStatus.Valid)
		// 			return;
		// 	}
		//
		// 	var error = GetErrorByTokenStatus(tokenStatus);
		// 	context.Result = new ObjectResult(error)
		// 	{
		// 		StatusCode = (int)error.HttpStatusCode,
		// 	};
		// }

		/// <summary>
		/// Creates error response from token validation status.
		/// </summary>
		/// <param name="tokenStatus">Token validation status.</param>
		/// <returns></returns>
		private ErrorResponse GetErrorByTokenStatus(TokenStatus tokenStatus)
			=> tokenStatus switch
			{
				TokenStatus.Expired => new ErrorResponse
				{
					HttpStatusCode = HttpStatusCode.Unauthorized,
					Message = ErrorMessages.Security.AccessTokenExpired,
					Code = ErrorCodes.Security.AccessTokenExpired
				},
				TokenStatus.Invalid => new ErrorResponse
				{
					HttpStatusCode = HttpStatusCode.Unauthorized,
					Message = ErrorMessages.Security.AccessTokenInvalid,
					Code = ErrorCodes.Security.AccessTokenInvalid
				},
				_ => new SecurityErrorResponse(),
			};

		/// <summary>
		/// Retrieves token from auth header.
		/// </summary>
		/// <param name="authHeader">Auth header value.</param>
		/// <returns></returns>
		private string GetCleanToken(string authHeader)
		{
			var index = authHeader.IndexOf(Bearer, StringComparison.CurrentCultureIgnoreCase);
			return index < 0 ? authHeader : authHeader.Remove(index, Bearer.Length);
		}
	}
}