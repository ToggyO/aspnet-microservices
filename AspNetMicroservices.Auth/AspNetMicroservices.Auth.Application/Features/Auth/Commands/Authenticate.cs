using System;
using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Validators;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Constants.Common;
using AspNetMicroservices.Shared.Models.Auth;
using AspNetMicroservices.Shared.Models.Settings;
using AspNetMicroservices.Shared.SharedServices.Cache;
using AspNetMicroservices.Shared.SharedServices.PasswordService;
using AspNetMicroservices.Shared.Utils;

using MapsterMapper;

using MediatR;

using Microsoft.Extensions.Options;

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
			/// Authentication tokens factory.
			/// </summary>
			private readonly ITokensFactory _factory;

			/// <summary>
			/// Password management service.
			/// </summary>
			private readonly IPasswordService _passwordService;

			/// <summary>
			/// Users repository.
			/// </summary>
			private readonly IUsersRepository _repository;

			/// <summary>
			/// Cache service.
			/// </summary>
			private readonly ICacheService _cache;

			/// <summary>
			/// Object mapper.
			/// </summary>
			private readonly IMapper _mapper;

			/// <summary>
			/// JWT settings
			/// </summary>
			private readonly JwtSettings _settings;

			/// <summary>
			/// Initialize new instance <see cref="Handler"/>.
			/// </summary>
			/// <param name="factory">Instance of <see cref="ITokensFactory"/>.</param>
			/// <param name="passwordService">Instance of <see cref="IPasswordService"/>.</param>
			/// <param name="repository">Instance of <see cref="IUsersRepository"/>.</param>
			/// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
			/// <param name="cache">Instance of <see cref="ICacheService"/>.</param>
			/// <param name="settings">Instance of <see cref="IOptions{JwtSettings}"/>.</param>
			public Handler(ITokensFactory factory,
				IPasswordService passwordService,
				IUsersRepository repository,
				IMapper mapper,
				ICacheService cache,
				IOptions<JwtSettings> settings)
			{
				_factory = factory;
				_passwordService = passwordService;
				_repository = repository;
				_mapper = mapper;
				_cache = cache;
				_settings = settings.Value;
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

				return await ProcessAuthentication(user);
			}

			private async Task<AuthenticationTicket<UserDto>> ProcessAuthentication(UserModel user)
			{
				var identityId = Utils.GenerateGuidString();
				var tokens = _factory.CreateToken(user, identityId);

				var ticket = new AuthenticationTicket<UserDto>
				{
					User = _mapper.From(user).AdaptToType<UserDto>(),
					Tokens = tokens
				};

				await _cache.SetCacheValueAsync($"{Prefix.Access}::{identityId}",
					ticket, TimeSpan.FromMinutes(_settings.AccessTokenExpiresInMinutes));

				await _cache.SetCacheValueAsync($"{Prefix.Refresh}::{tokens.RefreshToken}",
					new RefreshAuthTicketDto { Id = user.Id },
					TimeSpan.FromMinutes(_settings.RefreshTokenExpiresInMinutes));

				return ticket;
			}

			public class Validator : AuthenticateValidator<Command>
			{}

			// TODO: check or delete
			// public async Task<AuthenticationTicket<UserDto>> Handle(Command cmd, CancellationToken cancellationToken)
			// {
			// 	var user = await _repository.GetByEmail(cmd.Email);
			// 	if (user is null)
			// 		return default;
			//
			// 	bool isPasswordValid = _passwordService.VerifyPassword(new PasswordModel
			// 	{
			// 		Salt = user.Salt,
			// 		Hash = user.Hash,
			// 	}, cmd.Password);
			//
			// 	if (!isPasswordValid)
			// 		return default;
			//
			// 	var identityId = Utils.GenerateGuidString();
			// 	var ticket = new AuthenticationTicket<UserDto>
			// 	{
			// 		User = _mapper.From(user).AdaptToType<UserDto>(),
			// 		Tokens = _factory.CreateToken(user, identityId)
			// 	};
			//
			// 	await _cache.SetCacheValueAsync($"{Prefix.Access}::{identityId}", ticket);
			// 	return ticket;
			// }
		}
	}
}