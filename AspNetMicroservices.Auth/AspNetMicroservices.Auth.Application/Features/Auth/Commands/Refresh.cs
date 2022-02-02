using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Validators;
using AspNetMicroservices.Shared.Models.Auth;

using MediatR;

namespace AspNetMicroservices.Auth.Application.Features.Auth.Commands
{
	/// <summary>
	/// Refresh authentication ticket.
	/// </summary>
	public class Refresh
	{
		/// <summary>
		/// Refresh authentication ticket command.
		/// </summary>
		public class Command : RefreshTokenDto, IRequest<AuthenticationTicket<UserDto>>
		{}

		/// <summary>
		/// Refresh authentication ticket handler.
		/// </summary>
		public class Handler : IRequestHandler<Command, AuthenticationTicket<UserDto>>
		{
			public Task<AuthenticationTicket<UserDto>> Handle(Command request, CancellationToken cancellationToken)
			{
				throw new System.NotImplementedException();
			}
		}

		/// <summary>
		/// Refresh authentication ticket validator.
		/// </summary>
		public class Validator : RefreshTokenValidator
		{}
	}
}