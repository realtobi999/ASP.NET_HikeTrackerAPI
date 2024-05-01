using System.ComponentModel.DataAnnotations;

namespace HikingTracks.Domain;

public record class UpdateAccountDto
{
    [MaxLength(255)]
    public string? Username { get; set; }

    [MaxLength(255)]
    public string? Email { get; set; }

    [Range(0, int.MaxValue)]
    public int TotalHikes { get; set; }

    [Range(0, double.MaxValue)]
    public double TotalDistance { get; set; }

    public TimeSpan TotalMovingTime { get; set; }
};
