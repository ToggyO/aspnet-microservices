using System.Net;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Commands;
using AspNetMicroservices.Shared.Constants.Errors;
using AspNetMicroservices.Shared.Models.Auth;
using AspNetMicroservices.Shared.Models.Response;

using MediatR;

namespace AspNetMicroservices.Auth.Api.Handlers.Auth.Implementation
{
	/// <inheritdoc cref="IAuthHandler"/>.
	public class AuthHandler : IAuthHandler
	{
		/// <summary>
		/// Instance of <see cref="IMediator"/>.
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// Initialize new instance of <see cref="AuthHandler"/>.
		/// </summary>
		/// <param name="mediator">Application mediator.</param>
		public AuthHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <inheritdoc cref="IAuthHandler.Authenticate"/>.
		public async Task<Response<AuthenticationTicket<UserDto>>> Authenticate(Authenticate.Command cmd)
		{
			var ticket = await _mediator.Send(cmd);
			if (ticket is null)
				return new ErrorResponse<AuthenticationTicket<UserDto>>
				{
					Code = ErrorCodes.Security.AuthDataInvalid,
					Message = ErrorMessages.Security.AuthDataInvalid,
					HttpStatusCode = HttpStatusCode.Unauthorized,
				};

			return new Response<AuthenticationTicket<UserDto>> { Data = ticket };
		}

		/// <inheritdoc cref="IAuthHandler.RefreshToken"/>.
		public async Task<Response<TokenDto>> RefreshToken(Refresh.Command cmd)
		{
			var tokenDto = await _mediator.Send(cmd);
			if (tokenDto is null)
				return new ErrorResponse<TokenDto>
				{
					Code = ErrorCodes.Security.RefreshTokenInvalid,
					Message = ErrorMessages.Security.RefreshTokenInvalid,
					HttpStatusCode = HttpStatusCode.Unauthorized,
				};

			return new Response<TokenDto> { Data = tokenDto };
		}
	}
}