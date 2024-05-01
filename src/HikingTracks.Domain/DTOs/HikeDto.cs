using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain;

public record HikeDto
{
    public Guid ID { get; init; }         
    public Guid AccountID { get; init; }
    public double Distance { get; init; }
    public double ElevationGain { get; init; }
    public double ElevationLoss { get; init; }
    public double AverageSpeed { get; init; }
    public double MaxSpeed { get; init; }
    public TimeSpan MovingTime { get; init; }
    public List<Coordinate> Coordinates { get; init; } = [];
}
