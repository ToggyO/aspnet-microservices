using System;
using System.Text;

using AspNetMicroservices.Auth.Api.Handlers.Auth;
using AspNetMicroservices.Auth.Api.Handlers.Auth.Implementation;
using AspNetMicroservices.Auth.Api.Handlers.Users;
using AspNetMicroservices.Auth.Api.Handlers.Users.Implementation;
using AspNetMicroservices.Shared.Extensions;
using AspNetMicroservices.Shared.Models.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AspNetMicroservices.Auth.Api
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add api handlers module to an application services.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="serviceLifetime">Service lifetime enumeration.</param>
		public static void AddApiHandlers(this IServiceCollection services,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			services.Add<IUsersHandler, UsersHandler>(serviceLifetime);
			services.Add<IAuthHandler, AuthHandler>(serviceLifetime);
		}

		/// <summary>
		/// Configure application settings.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="configuration">Application configuration.</param>
		/// <param name="isDevelopment">
		/// Indicates, whether application is running in development environment.
		/// </param>
		public static void AddSettings(this IServiceCollection services,
			IConfiguration configuration, bool isDevelopment = true)
		{
			services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);
			services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
			services.Configure<RedisSettings>(o =>
			{
				o.Host = DotNetEnv.Env.GetString("AUTH_REDIS_HOST");
				o.Db = DotNetEnv.Env.GetInt("AUTH_REDIS_DATABASE");
				o.Password = DotNetEnv.Env.GetString("AUTH_REDIS_PASSWORD");
				o.Port = DotNetEnv.Env.GetInt(isDevelopment ? "AUTH_REDIS_EXTERNAL_PORT" : "AUTH_REDIS_PORT");
				o.KeyExpirationInSec = DotNetEnv.Env.GetInt("AUTH_REDIS_KEY_EXPIRATION_IN_SEC");
			});
		}

		/// <summary>
		/// Add authentication and authorization an application services.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="configuration">Application configuration.</param>
		/// <param name="serviceLifetime">Service lifetime enumeration.</param>
		public static void AddAuthServices(this IServiceCollection services,
			IConfiguration configuration,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
			services.AddAuthentication(o =>
				{
					o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(o =>
				{
					o.TokenValidationParameters = new TokenValidationParameters
					{
						ClockSkew = TimeSpan.Zero,

						ValidateAudience = true,
						ValidAudience = jwtSettings.Audience,

						ValidateIssuer = true,
						ValidIssuer = jwtSettings.Issuer,

						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.PrivateKey)),
						ValidateIssuerSigningKey = true,

						// To allow return custom response for expired token
						ValidateLifetime = false
					};
				});

			services.AddAuthorization();
		}
	}
}