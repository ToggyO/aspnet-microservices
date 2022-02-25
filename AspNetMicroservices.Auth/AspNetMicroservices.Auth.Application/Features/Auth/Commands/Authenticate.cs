using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Validators;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Common.Utils;
using AspNetMicroservices.SharedServices.PasswordService;

using MediatR;

using Microsoft.Extensions.Logging;

namespace AspNetMicroservices.Auth.Application.Features.Auth.Commands
{
	/// <summary>
	/// Authenticate user.
	/// </summary>
	public class Authenticate
	{
		/// <summary>
		/// Authenticate user command.
		/// </summary>
		public class Command : CredentialsDto, IRequest<AuthenticationTicket<UserDto>>
		{}

		/// <summary>
		/// Authenticate user command handler.
		/// </summary>
		public class Handler : IRequestHandler<Command, AuthenticationTicket<UserDto>>
		{
			/// <summary>
			/// Password management service.
			/// </summary>
			private readonly IPasswordService _passwordService;

			/// <summary>
			/// Authentication process handler.
			/// </summary>
			private readonly IAuthenticationService _authService;

			/// <summary>
			/// Users repository.
			/// </summary>
			private readonly IUsersRepository _repository;

			/// <summary>
			/// Initialize new instance <see cref="Handler"/>.
			/// </summary>
			/// <param name="passwordService">Instance of <see cref="IPasswordService"/>.</param>
			/// <param name="authenticationService">Instance of <see cref="IAuthenticationService"/>.</param>
			/// <param name="repository">Instance of <see cref="IUsersRepository"/>.</param>
			public Handler(IPasswordService passwordService,
				IAuthenticationService authenticationService,
				IUsersRepository repository)
			{
				_passwordService = passwordService;
				_authService = authenticationService;
				_repository = repository;
			}

			/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}.Handle"/>.
			public async Task<AuthenticationTicket<UserDto>> Handle(Command cmd, CancellationToken cancellationToken)
			{
				var user = await _repository.GetByEmail(cmd.Email);
				if (user is null)
					return default;

				bool isPasswordValid = _passwordService.VerifyPassword(new PasswordModel
				{
					Salt = user.Salt,
					Hash = user.Hash,
				}, cmd.Password);

				if (!isPasswordValid)
					return default;

				return await _authService.HandleAuthentication(user);
			}

			public class Validator : AuthenticateValidator<Command>
			{}
		}
	}
}