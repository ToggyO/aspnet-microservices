using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;

namespace AspNetMicroservices.Auth.Application.Common.Interfaces
{
	// TODO: delete
	/// <summary>
	/// Represents authentication tokens management process.
	/// </summary>
	public interface ITokensService
	{
		/// <summary>
		/// Authenticate user and creates authentication information for authenticated user.
		/// </summary>
		/// <param name="credentials"></param>
		/// <returns></returns>
		Task<AuthenticationTicket<UserDto>> Authenticate(CredentialsDto credentials);

		Task<TokenDto> RefreshToken(string token);
	}
}