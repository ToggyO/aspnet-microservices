using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Commands;

namespace AspNetMicroservices.Auth.Api.Handlers.Auth
{
	/// <summary>
	/// Http request handler for authentication and authorization.
	/// </summary>
	public interface IAuthHandler
	{
		/// <summary>
		/// Authenticate user handler.
		/// </summary>
		/// <param name="cmd">Authentication credentials command.</param>
		/// <returns></returns>
		Task<Response<AuthenticationTicket<UserDto>>> Authenticate(Authenticate.Command cmd);

		/// <summary>
		/// Refresh authentication tokens for user.
		/// </summary>
		/// <param name="cmd">Refresh token command.</param>
		/// <returns></returns>
		Task<Response<TokenDto>> RefreshToken(Refresh.Command cmd);
	}
}