using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.DTO;

public record HikeDto
{
    public Guid ID { get; set; }         
    public Guid AccountId { get; set; }
    public double Distance { get; set; }
    public double ElevationGain { get; set; }
    public double ElevationLoss { get; set; }
    public double AverageSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public TimeSpan MovingTime { get; set; }
    public List<Coordinate> Coordinates { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; }
    public List<Photo> Photos { get; set; } = [];
    public List<SegmentDto> Segments { get; set; } = [];
}
