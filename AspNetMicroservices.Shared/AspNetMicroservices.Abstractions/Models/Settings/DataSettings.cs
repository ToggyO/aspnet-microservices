namespace AspNetMicroservices.Abstractions.Models.Settings
{
	/// <summary>
	/// Common database connection configuration settings.
	/// </summary>
	public class DataSettings
	{
		/// <summary>
		/// Host to connect to.
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// Port to connect to.
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		/// Database user.
		/// </summary>
		public string User { get; set; }

		/// <summary>
		/// Database user password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Name of database to connect to.
		/// </summary>
		public string DbName { get; set; }
	}
}