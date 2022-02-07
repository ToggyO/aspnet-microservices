namespace AspNetMicroservices.Shared.Models.Settings
{
	public class RedisSettings
	{
		// TODO: разобраться с дефолтной БД
		public string ConnectionString => $"{Host}:{Port},password={Password},defaultDatabase={Db}";

		public string Host { get; set; } = "localhost";

		public int Port { get; set; } = 6379;

		public string Password { get; set; }

		public int Db { get; set; }

		public double KeyExpirationInSec { get; set; } = 60 * 60; // 1 hour
	}
}