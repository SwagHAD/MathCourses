using System.ComponentModel.DataAnnotations;

namespace SharedKernel.AspNetCore.Options
{
    public sealed class JwtOptions
    {
        public const string SectionName = "Jwt";

        [Required]
        [MinLength(32)]
        public string SecretKey { get; init; } = null!;

        [Required]
        public string Issuer { get; init; } = null!;

        [Required]
        public string Audience { get; init; } = null!;

        [Required]
        [Range(1, 1440)]
        public int AccessTokenLifetimeMinutes { get; init; }
    }
}
