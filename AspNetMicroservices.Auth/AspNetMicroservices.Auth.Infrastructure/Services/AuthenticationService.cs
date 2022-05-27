using System;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Abstractions.Models.Settings;
using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Common.Constants.Common;
using AspNetMicroservices.Common.Utils;
using AspNetMicroservices.SharedServices.Cache;

using MapsterMapper;

using Microsoft.Extensions.Options;

namespace AspNetMicroservices.Auth.Infrastructure.Services
{
    /// <summary>
    /// Represents authentication process handling and caching auth data.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
	    /// <summary>
	    /// Authentication tokens factory.
	    /// </summary>
        private readonly ITokensFactory _factory;

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
        /// Initialize new instance <see cref="AuthenticationService"/>.
        /// </summary>
        /// <param name="factory">Instance of <see cref="ITokensFactory"/>.</param>
        /// <param name="cache">Instance of <see cref="ICacheService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="settings">Instance of <see cref="IOptions{TOptions}"/>.</param>
        public AuthenticationService(ITokensFactory factory,
	        ICacheService cache,
	        IMapper mapper,
	        IOptions<JwtSettings> settings)
        {
	        _factory = factory;
	        _cache = cache;
	        _mapper = mapper;
	        _settings = settings.Value;
        }

        /// <inheritdoc cref="IAuthenticationService.HandleAuthentication"/>.
        public async Task<AuthenticationTicket<UserDto>> HandleAuthentication(UserModel user)
        {
            var identityId = Utils.GenerateGuidString();
            var tokens = _factory.CreateToken(user, identityId);

            var ticket = new AuthenticationTicket<UserDto>
            {
                User = _mapper.From(user).AdaptToType<UserDto>(),
                Tokens = tokens
            };

            var setAccessTask = _cache.SetCacheValueAsync(Utils.CreateCacheKey(Prefix.Access, identityId),
                ticket, TimeSpan.FromMinutes(_settings.AccessTokenExpiresInMinutes));

            var setRefreshTask = _cache.SetCacheValueAsync( Utils.CreateCacheKey(Prefix.Refresh, tokens.RefreshToken),
                new RefreshAuthTicketDto { Id = user.Id, AuthenticationTicketId = identityId },
                TimeSpan.FromMinutes(_settings.RefreshTokenExpiresInMinutes));

            await Task.WhenAll(setAccessTask, setRefreshTask);

            return ticket;
        }
    }
}