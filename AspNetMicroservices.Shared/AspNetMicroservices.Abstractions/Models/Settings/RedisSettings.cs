namespace AspNetMicroservices.Abstractions.Models.Settings
{
	/// <summary>
	/// Redis connection configuration settings.
	/// </summary>
	public class RedisSettings
	{
		// TODO: разобраться с дефолтной БД
		/// <summary>
		/// Redis connection string.
		/// </summary>
		public string ConnectionString => $"{Host}:{Port},password={Password},defaultDatabase={Db}";

		/// <summary>
		/// Host to connect to.
		/// </summary>
		public string Host { get; set; } = "localhost";

		/// <summary>
		/// Port to connect to.
		/// </summary>
		public int Port { get; set; } = 6379;

		/// <summary>
		/// Password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Database name.
		/// </summary>
		public int Db { get; set; }

		/// <summary>
		/// Record expiration time.
		/// </summary>
		public double KeyExpirationInSec { get; set; } = 60 * 60; // 1 hour
	}
}