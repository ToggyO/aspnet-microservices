using AspNetMicroservices.Auth.Application.Common.Interfaces;
using AspNetMicroservices.Auth.Infrastructure.Factories;
using AspNetMicroservices.Shared.Extensions;
using AspNetMicroservices.Shared.SharedServices.PasswordService;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Auth.Infrastructure
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add infrastructure module to an application.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="serviceLifetime">Service lifetime.</param>
		public static void AddInfrastructure(this IServiceCollection services,
			ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			services.Add<IPasswordService, PasswordService>(serviceLifetime);

			services.Add<ITokensFactory, JwtTokensFactory>(serviceLifetime);
		}
	}
}