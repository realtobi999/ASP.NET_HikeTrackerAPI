using System.ComponentModel.DataAnnotations;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.DTO;

public record class CreateAccountDto
{
    public Guid? ID { get; set; }

    [Required, MaxLength(255)]
    public string? Username { get; set; }

    [Required, MaxLength(255)]
    public string? Email { get; set; }

    [Required, MinLength(8), MaxLength(255)]
    public string? Password { get; set; }
}
