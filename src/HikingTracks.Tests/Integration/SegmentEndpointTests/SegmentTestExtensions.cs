using Bogus;
using HikingTracks.Application;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Tests;

public static class SegmentTestExtensions
{
    private static readonly Faker<Segment> _segmentFaker = new Faker<Segment>()
            .RuleFor(h => h.ID, f => f.Random.Guid())
            .RuleFor(h => h.Name, f => f.Random.AlphaNumeric(32))
            .RuleFor(h => h.Distance, f => f.Random.Double(0, 100))
            .RuleFor(h => h.ElevationGain, f => f.Random.Double(0, 100))
            .RuleFor(h => h.ElevationLoss, f => f.Random.Double(0, 100))
            .RuleFor(h => h.Coordinates, f => CoordinateService.GenerateRandomCoordinatesList(50))
            .RuleFor(h => h.CreatedAt, f => DateTimeOffset.UtcNow);

    public static Segment WithFakeData(this Segment segment)
    {
        segment = _segmentFaker.Generate();
        return segment;
    }

    public static CreateSegmentDto ToCreateSegmentDto(this Segment segment)
    {
        return new CreateSegmentDto{
            ID = segment.ID,
            Name = segment.Name,
            Distance = segment.Distance,
            ElevationGain = segment.ElevationGain,
            ElevationLoss = segment.ElevationLoss,
            Coordinates = segment.Coordinates,
        };
    }
}
