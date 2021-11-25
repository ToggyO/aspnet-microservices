using System.Data;

namespace AspNetMicroservices.Auth.Domain.Models.Database
{
	/// <summary>
	/// Database connection factory.
	/// </summary>
	public interface IDbConnectionFactory<out T> where T : IDbConnection
	{
		/// <summary>
		/// Creates an instance of database connection.
		/// </summary>
		/// <returns>Instance of <see cref="IDbConnection"/>.</returns>
		T GetDbConnection();
	}
}