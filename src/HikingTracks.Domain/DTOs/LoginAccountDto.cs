using System.ComponentModel.DataAnnotations;

namespace HikingTracks.Domain;

public record class LoginAccountDto
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
