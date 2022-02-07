using System;
using System.Text;

using AspNetMicroservices.Shared.Models.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AspNetMicroservices.Products.Api
{
	public static class DependencyInjection
	{
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