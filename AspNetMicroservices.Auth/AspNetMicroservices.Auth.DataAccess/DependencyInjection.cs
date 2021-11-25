using AspNetMicroservices.Auth.DataAccess.Context;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Auth.DataAccess
{
	public static class DependencyInjection
	{
		/// <summary>
		///  Add data access module to an application.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="connectionString">Database connection string.</param>
		/// <param name="serviceLifetime">Service lifetime.</param>
		public static void AddDataAccess(this IServiceCollection services,
			string connectionString, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			services.AddScoped(x => new AuthServiceDbContext(connectionString));
		}
	}
}