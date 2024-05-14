using System.ComponentModel.DataAnnotations;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.DTO;

public record class CreateSegmentDto
{
    public Guid? ID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public double Distance { get; set; }

    [Required]
    public double ElevationGain { get; set; }

    [Required]
    public double ElevationLoss { get; set; }

    [Required]
    public List<Coordinate> Coordinates { get; set; } = [];
}
