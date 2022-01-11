using System.Threading.Tasks;

using AspNetMicroservices.Auth.Api.Handlers.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Commands;
using AspNetMicroservices.Shared.Models.Auth;
using AspNetMicroservices.Shared.Models.Response;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Auth.Api.Controllers.v1
{
	[ApiController]
	[Route("auth")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthHandler _handler;

		public AuthController(IAuthHandler handler)
		{
			_handler = handler;
		}

		[AllowAnonymous]
		[HttpPost("sign-in")]
		public async Task<Response<AuthenticationTicket<UserDto>>> Authenticate([FromBody] Authenticate.Command cmd)
			=> await _handler.Authenticate(cmd);
	}
}