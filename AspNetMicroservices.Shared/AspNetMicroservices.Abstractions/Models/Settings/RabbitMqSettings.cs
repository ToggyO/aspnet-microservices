namespace AspNetMicroservices.Abstractions.Models.Settings
{
	/// <summary>
	/// RabbitMQ connection configuration settings.
	/// </summary>
    public class RabbitMqSettings
    {
	    /// <summary>
	    /// Host to connect to.
	    /// </summary>
        public string Host { get; set; }

	    /// <summary>
	    /// Port to connect to.
	    /// </summary>
        public string Port { get; set; }

	    /// <summary>
	    /// User name.
	    /// </summary>
        public string Username { get; set; }

	    /// <summary>
	    /// Password.
	    /// </summary>
        public string Password { get; set; }

	    /// <summary>
	    /// RabbitMQ connection string.
	    /// </summary>
        public string RabbitMqConnectionString => $"amqp://{Username}:{Password}@{Host}:{Port}";
    }
}