using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Features.Auth.Validators;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Common.Constants.Common;
using AspNetMicroservices.Common.Utils;
using AspNetMicroservices.SharedServices.Cache;

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
		public class Command : RefreshTokenDto, IRequest<TokenDto>
		{}

		/// <summary>
		/// Refresh authentication ticket handler.
		/// </summary>
		public class Handler : IRequestHandler<Command, TokenDto>
		{
			/// <summary>
			/// Users repository.
			/// </summary>
			private readonly IUsersRepository _repository;

			/// <summary>
			/// Cache service.
			/// </summary>
			private readonly ICacheService _cache;

			/// <summary>
			/// Authentication process handler.
			/// </summary>
			private readonly IAuthenticationService _authService;

			/// <summary>
			/// Initialize new instance <see cref="Handler"/>.
			/// </summary>
			/// <param name="repository">Instance of <see cref="IUsersRepository"/>.</param>
			/// <param name="cache">Instance of <see cref="ICacheService"/>.</param>
			public Handler(IUsersRepository repository,
				ICacheService cache,
				IAuthenticationService authenticationService)
			{
				_repository = repository;
				_cache = cache;
				_authService = authenticationService;
			}

			/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}.Handle"/>.
			public async Task<TokenDto> Handle(Command cmd, CancellationToken cancellationToken)
			{
				var refreshAuthTicket = await _cache.GetCacheValueAsync<RefreshAuthTicketDto>($"{Prefix.Refresh}::{cmd.RefreshToken}");
				if (refreshAuthTicket is null)
					return default;

				var user = await _repository.GetById(refreshAuthTicket.Id);
				if (user is null)
					return default;

				var ticket = await _authService.HandleAuthentication(user);
				await _cache.RemoveCacheValueAsync(Utils.CreateCacheKey(Prefix.Access, refreshAuthTicket.AuthenticationTicketId));
				return ticket.Tokens;
			}
		}

		/// <summary>
		/// Refresh authentication ticket validator.
		/// </summary>
		public class Validator : RefreshTokenValidator<Command>
		{}
	}
}