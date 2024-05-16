using System.ComponentModel.DataAnnotations;

namespace HikingTracks.Domain.DTO;

public record class UpdateAccountDto
{
    [Required, MaxLength(255)]
    public string? Username { get; set; }

    [Required, MaxLength(255)]
    public string? Email { get; set; }
};
