using System.ComponentModel.DataAnnotations;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain;

public class UpdateSegmentDto   
{
    [Required]
    public string? Name { get; set; }

    [Required, Range(0, double.MaxValue)]
    public double Distance { get; set; }

    [Required, Range(0, double.MaxValue)]
    public double ElevationGain { get; set; }

    [Required, Range(0, double.MaxValue)]
    public double ElevationLoss { get; set; }

    public List<Coordinate> Coordinates { get; set; } = [];
}
