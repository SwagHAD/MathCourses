using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Messaging.Options
{
    public sealed class RabbitMqOptions
    {
        public const string SectionName = "RabbitMq";

        [Required]
        public string Host { get; init; }

        [Required]
        public string Username { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
