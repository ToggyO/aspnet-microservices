using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Infrastructure.Factories;
using AspNetMicroservices.Auth.Infrastructure.Services;
using AspNetMicroservices.Extensions.ServiceCollection;
using AspNetMicroservices.Logging.Serilog;
// using AspNetMicroservices.Logging.Serilog;
using AspNetMicroservices.SharedServices.PasswordService;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Auth.Infrastructure
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add infrastructure module to an application.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="configuration">Instance of <see cref="IConfiguration"/>.</param>
		/// <param name="serviceLifetime">Service lifetime.</param>
		public static void AddInfrastructure(this IServiceCollection services,
			IConfiguration configuration,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			services.AddConfiguredSerilog(configuration);

			services.Add<IPasswordService, PasswordService>(serviceLifetime);
			services.Add<IAuthenticationService, AuthenticationService>(serviceLifetime);

			services.Add<ITokensFactory, JwtTokensFactory>(serviceLifetime);
		}
	}
}