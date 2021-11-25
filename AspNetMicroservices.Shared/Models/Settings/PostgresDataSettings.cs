namespace AspNetMicroservices.Shared.Models.Settings
{
	public class PostgresDataSettings : DataSettings
	{
		public string DbConnectionString =>
			$"Host={Host};Port={Port};Username={User};Password={Password};Database={DbName}"; // PostgresSQL
	}
}