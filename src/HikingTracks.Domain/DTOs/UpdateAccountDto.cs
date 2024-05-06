using System.ComponentModel.DataAnnotations;

namespace HikingTracks.Domain.DTO;

public record class UpdateAccountDto
{
    [MaxLength(255)]
    public string? Username { get; set; }

    [MaxLength(255)]
    public string? Email { get; set; }
};
