namespace AspNetMicroservices.Shared.Models.Settings
{
	/// <summary>
	/// PostgreSQL database connection configuration settings.
	/// </summary>
	public class PostgresDataSettings : DataSettings
	{
		/// <summary>
		/// Concatenated connection string.
		/// </summary>
		public string DbConnectionString =>
			$"Host={Host};Port={Port};Username={User};Password={Password};Database={DbName};"; // PostgresSQL
	}
}