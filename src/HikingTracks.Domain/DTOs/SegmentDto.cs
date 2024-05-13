using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.DTO;

public record class SegmentDto
{
    public Guid ID { get; set; }
    public string? Name { get; set; }
    public double Distance { get; set; }
    public double ElevationGain { get; set; }
    public double ElevationLoss { get; set; }
    public List<Coordinate> Coordinates { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; }
}
