namespace AspNetMicroservices.Products.Common.Settings
{
    public class DbSettings
    {
        public string DbConnectionString =>
            $"Host={DB_HOST};Port={DB_PORT};Username={DB_USER};Password={DB_PASSWORD};Database={DB_NAME}"; // PosgreSQL

        public string DB_HOST { get; set; }
        public string DB_PORT { get; set; }
        public string DB_USER { get; set; }
        public string DB_PASSWORD { get; set; }
        public string DB_NAME { get; set; }
    }
}