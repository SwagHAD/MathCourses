using System.ComponentModel.DataAnnotations;

namespace Auth.Seeds.Options;

public sealed class SeedOptions
{
    public const string SectionName = "Seed";

    [Required]
    public string AdminLogin { get; init; }

    [Required]
    [MinLength(3)]
    public string AdminPassword { get; init; }
}
