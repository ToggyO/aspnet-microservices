using System.Net;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Auth.Api.Handlers.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Auth.Api.Controllers.v1
{
	/// <summary>
	/// Http request controller for authentication and authorization.
	/// </summary>
	[ApiController]
	[Route("auth")]
	[ApiVersion("1.0")]
	public class AuthController : ControllerBase, IAuthHandler
	{
		/// <summary>
		/// Authentication handler.
		/// </summary>
		private readonly IAuthHandler _handler;

		/// <summary>
		/// Initialize new instance of <see cref="AuthController"/>.
		/// </summary>
		/// <param name="handler">Instance of <see cref="IAuthHandler"/>.</param>
		public AuthController(IAuthHandler handler)
		{
			_handler = handler;
		}

		/// <inheritdoc cref="IAuthHandler.Authenticate"/>.
		[AllowAnonymous]
		[HttpPost("sign-in")]
		[ProducesResponseType(typeof(Response<AuthenticationTicket<UserDto>>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(SecurityErrorResponse), (int)HttpStatusCode.Unauthorized)]
		public async Task<Response<AuthenticationTicket<UserDto>>> Authenticate([FromBody] Authenticate.Command cmd)
			=> await _handler.Authenticate(cmd);

		/// <inheritdoc cref="IAuthHandler.RefreshToken"/>
		[AllowAnonymous]
		[HttpPost("refresh")]
		[ProducesResponseType(typeof(Response<TokenDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(SecurityErrorResponse), (int)HttpStatusCode.Unauthorized)]
		public async Task<Response<TokenDto>> RefreshToken([FromBody] Refresh.Command cmd)
			=> await _handler.RefreshToken(cmd);

		[HttpGet("check")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public bool CheckAuthentication() => true;
	}
}