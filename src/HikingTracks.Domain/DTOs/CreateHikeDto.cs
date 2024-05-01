using System.ComponentModel.DataAnnotations;

namespace HikingTracks.Domain;

public record class CreateHikeDto
{
    [Required, Range(0, double.MaxValue)]
    public double Distance { get; init; }

    [Required, Range(0, double.MaxValue)]
    public double ElevationGain { get; init; }

    [Required, Range(0, double.MaxValue)]
    public double ElevationLoss { get; init; }

    [Required, Range(0, double.MaxValue)]
    public double MaxSpeed { get; init; }

    [Required]
    public TimeSpan MovingTime { get; init; }

    [Required]
    public required string CoordinatesString { get; init; }
}