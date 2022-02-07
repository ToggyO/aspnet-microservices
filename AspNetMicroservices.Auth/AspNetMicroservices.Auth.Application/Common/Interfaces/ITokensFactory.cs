using System;
using System.Security.Claims;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Common.Enums;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Shared.Models.Auth;

namespace AspNetMicroservices.Auth.Application.Common.Interfaces
{
	/// <summary>
	/// Authentication tokens factory.
	/// </summary>
	public interface ITokensFactory
	{
		/// <summary>
		/// Creates authentication tokens.
		/// </summary>
		/// <param name="user">User model.</param>
		/// <param name="identityId">Stringed guid value.</param>
		/// <returns></returns>
		TokenDto CreateToken(UserModel user, string identityId);

		/// <summary>
		/// Validates authentication token.
		/// </summary>
		/// <param name="token">Authentication token.</param>
		/// <param name="principal">Instance of <see cref="ClaimsPrincipal"/>.</param>
		/// <returns></returns>
		TokenStatus ValidateToken(string token, out ClaimsPrincipal principal);
	}
}