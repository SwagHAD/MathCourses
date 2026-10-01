using System.ComponentModel.DataAnnotations;

namespace Infrasctrure.Options;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required]
    [MinLength(32)]
    public string SecretKey { get; init; }

    [Required]
    public string Issuer { get; init; }

    [Required]
    public string Audience { get; init; }

    [Required]
    [Range(1, 1440)]
    public int AccessTokenLifetimeMinutes { get; init; }
}
