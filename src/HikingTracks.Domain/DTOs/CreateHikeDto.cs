using System.ComponentModel.DataAnnotations;

namespace HikingTracks.Domain;

public record class CreateHikeDto
{
    [Required, Range(0, double.MaxValue)]
    public double Distance { get; set; }

    [Required, Range(0, double.MaxValue)]
    public double ElevationGain { get; set; }

    [Required, Range(0, double.MaxValue)]
    public double ElevationLoss { get; set; }

    [Required, Range(0, double.MaxValue)]
    public double MaxSpeed { get; set; }

    [Required]
    public TimeSpan MovingTime { get; set; }

    [Required]
    public required string CoordinatesString { get; set; }
}