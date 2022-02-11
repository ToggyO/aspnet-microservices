using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Abstractions.Models.Settings;
using AspNetMicroservices.Auth.Application.Common.Enums;
using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using ClaimTypes = AspNetMicroservices.Common.Constants.Claims.ClaimTypes;

namespace AspNetMicroservices.Auth.Infrastructure.Factories
{
	/// <summary>
	/// JWT tokens factory.
	/// </summary>
	public class JwtTokensFactory : ITokensFactory
	{
		private const string Algorithm = SecurityAlgorithms.HmacSha256;

		private readonly JwtSecurityTokenHandler _handler = new ();

		private readonly TokenValidationParameters _tokenValidationParameters;

		private readonly JwtSettings _settings;

		private readonly byte[] _privateKeyBinary;

		/// <summary>
		/// Initialize new instance of <see cref="JwtTokensFactory"/>.
		/// </summary>
		/// <param name="settings">JWT settings.</param>
		public JwtTokensFactory(IOptions<JwtSettings> settings)
		{
			_settings = settings.Value;
			_privateKeyBinary = Encoding.UTF8.GetBytes(_settings.PrivateKey);
			_tokenValidationParameters = CreateTokenValidationParameters();
		}

		/// <inheritdoc cref="ITokensFactory.CreateToken"/>.
		public virtual TokenDto CreateToken(UserModel user, string identityId)
		{
			var now = DateTime.Now;
			var accessExp = now.AddMinutes(_settings.AccessTokenExpiresInMinutes);
			var refreshExp = now.AddMinutes(_settings.RefreshTokenExpiresInMinutes);

			return new TokenDto
			{
				AccessToken = _handler.WriteToken(CreateJwtSecurityToken(user, identityId, accessExp)),
				AccessTokenExpire = accessExp,
				RefreshToken = _handler.WriteToken(CreateJwtSecurityToken(user, identityId, refreshExp)),
				RefreshTokenExpire = refreshExp
			};
		}

		/// <inheritdoc cref="ITokensFactory.ValidateToken"/>.
		public virtual TokenStatus ValidateToken(string token, out ClaimsPrincipal principal)
		{
			try
			{
				principal = _handler.ValidateToken(token,
					_tokenValidationParameters, out SecurityToken securityToken);

				if (DateTime.UtcNow > securityToken?.ValidTo)
					return TokenStatus.Expired;

				bool isValid = securityToken is JwtSecurityToken jwt &&
				               jwt.Header.Alg.Equals(Algorithm, StringComparison.OrdinalIgnoreCase);

				return isValid ? TokenStatus.Valid : TokenStatus.Invalid;
			}
			catch (Exception)
			{
				principal = null;
				return TokenStatus.Invalid;
			}
		}

		/// <summary>
		/// Creates instance of <see cref="JwtSecurityToken"/> with specified claims.
		/// </summary>
		/// <param name="user">Portal user.</param>
		/// <param name="identityId">Stringed guid value.</param>
		/// <param name="exp">Token expiration time.</param>
		/// <returns></returns>
		protected virtual JwtSecurityToken CreateJwtSecurityToken(UserModel user,
			string identityId, DateTime exp)
		{
			return new JwtSecurityToken(
				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(_privateKeyBinary), Algorithm),
				audience: _settings.Audience,
				issuer: _settings.Issuer,
				expires: exp,
				claims: new []
				{
					new Claim(ClaimTypes.IdentityId, identityId)
				});
		}

		private TokenValidationParameters CreateTokenValidationParameters()
			=> new TokenValidationParameters
			{
				ValidateAudience = true,
				ValidAudience = _settings.Audience,

				ValidateIssuer = true,
				ValidIssuer = _settings.Issuer,

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(_privateKeyBinary),

				ValidateLifetime = false
			};
	}
}