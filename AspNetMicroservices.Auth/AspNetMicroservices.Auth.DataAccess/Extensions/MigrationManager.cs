using System;

using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetMicroservices.Auth.DataAccess.Extensions
{
	/// <summary>
	/// Migrator extensions.
	/// </summary>
	public static class MigrationManager
	{
		/// <summary>
		/// Migrate database.
		/// </summary>
		/// <param name="host">Instance of <see cref="IHost"/>.</param>
		/// <returns></returns>
		public static IHost MigrateDatabase(this IHost host)
		{
			using var scope = host.Services.CreateScope();
			var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

			try
			{
				migrationRunner.ListMigrations();
				migrationRunner.MigrateUp();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return host;
		}
	}
}