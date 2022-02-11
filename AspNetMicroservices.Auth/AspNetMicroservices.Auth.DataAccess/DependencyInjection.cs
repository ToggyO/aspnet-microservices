using System.Reflection;

using AspNetMicroservices.Auth.DataAccess.Context;
using AspNetMicroservices.Auth.DataAccess.Mapping;
using AspNetMicroservices.Auth.DataAccess.Repositories;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Extensions.ServiceCollection;

using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Auth.DataAccess
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add data access module to an application services.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="connectionString">Database connection string.</param>
		/// <param name="serviceLifetime">Service lifetime.</param>
		public static void AddDataAccess(this IServiceCollection services,
			string connectionString, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
		{
			services.AddScoped(x => new AuthServiceDbContext(connectionString));
			services.AddFluentMigratorCore()
				.ConfigureRunner(cfg => cfg
					.AddPostgres()
					.WithGlobalConnectionString(connectionString)
					.ScanIn(Assembly.GetExecutingAssembly()).For.All())
				.AddLogging(cfg => cfg.AddFluentMigratorConsole());

			DbModelsMapping.Initialize();

			services.Add<IUsersRepository, UsersRepository>(serviceLifetime);
		}
	}
}