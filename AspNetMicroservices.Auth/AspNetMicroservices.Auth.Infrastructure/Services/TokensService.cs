using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Shared.Models.Auth;

namespace AspNetMicroservices.Auth.Infrastructure.Services
{
	// TODO: delete
	public class TokensService : ITokensService
	{
		public Task<AuthenticationTicket<UserDto>> Authenticate(CredentialsDto credentials)
		{
			throw new System.NotImplementedException();
		}

		public Task<TokenDto> RefreshToken(string token)
		{
			throw new System.NotImplementedException();
		}
	}
}