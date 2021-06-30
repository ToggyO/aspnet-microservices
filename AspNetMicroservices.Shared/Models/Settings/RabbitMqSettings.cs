namespace AspNetMicroservices.Shared.Models.Settings
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string RabbitMqConnectionString => $"amqp://{Username}:{Password}@{Host}:{Port}";
    }
}