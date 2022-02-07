using AspNetMicroservices.Auth.Domain.Models.Database;

using Npgsql;

namespace AspNetMicroservices.Auth.DataAccess.Context
{
	public class AuthServiceDbContext : IDbConnectionFactory<NpgsqlConnection>
	{
		/// <summary>
		/// Database connection string.
		/// </summary>
		private readonly string _connectionString;

		/// <summary>
		/// Initialize new instance of <see cref="AuthServiceDbContext"/>.
		/// </summary>
		/// <param name="connectionString">Database connection string.</param>
		public AuthServiceDbContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		/// <inheritdoc cref="IDbConnectionFactory{T}.GetDbConnection"/>
		/// <returns>An instance of postgresql database connection.</returns>
		public NpgsqlConnection GetDbConnection()
		{
			return new (_connectionString);
		}
	}
}