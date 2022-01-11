using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Commands;
using AspNetMicroservices.Shared.Models.Auth;
using AspNetMicroservices.Shared.Models.Response;

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
		/// <param name="cmd">Authenticate user command.</param>
		/// <returns></returns>
		Task<Response<AuthenticationTicket<UserDto>>> Authenticate(Authenticate.Command cmd);

		/// <summary>
		/// Refresh authentication tokens for user.
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<Response<TokenDto>> RefreshToken(string token);
	}
}