using Bogus;
using HikingTracks.Application;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Tests;

public static class HikeTestExtensions
{
    private static readonly Faker<Hike> _hikeFaker = new Faker<Hike>()
            .RuleFor(h => h.ID, f => f.Random.Guid())
            .RuleFor(h => h.AccountId, f => f.Random.Guid())
            .RuleFor(h => h.Title, f => f.Random.AlphaNumeric(32))
            .RuleFor(h => h.Description, f => f.Random.AlphaNumeric(123))
            .RuleFor(h => h.Distance, f => f.Random.Double(0, 100))
            .RuleFor(h => h.ElevationGain, f => f.Random.Double(0, 500))
            .RuleFor(h => h.ElevationLoss, f => f.Random.Double(0, 500))
            .RuleFor(h => h.AverageSpeed, f => f.Random.Double(0, 20))
            .RuleFor(h => h.MaxSpeed, f => f.Random.Double(0, 30))
            .RuleFor(h => h.MovingTime, f => f.Date.Timespan())
            .RuleFor(h => h.Coordinates, f => CoordinateService.GenerateRandomCoordinatesList(50))
            .RuleFor(h => h.CreatedAt, f => DateTimeOffset.UtcNow);

    public static Hike WithFakeData(this Hike hike)
    {
        hike = _hikeFaker.Generate();
        return hike;
    }

    public static Hike WithFakeData(this Hike hike, Account account)
    {
        hike = _hikeFaker.Generate();
        hike.AccountId = account.ID;
        return hike;
    }

    public static CreateHikeDto ToCreateHikeDto(this Hike hike)
    {
        return new CreateHikeDto{
            ID = hike.ID,
            AccountId = hike.AccountId,
            Title = hike.Title,
            Description = hike.Description,
            Distance = hike.Distance,
            ElevationGain = hike.ElevationGain,
            ElevationLoss = hike.ElevationLoss,
            MaxSpeed = hike.MaxSpeed,
            MovingTime = hike.MovingTime,
            Coordinates = hike.Coordinates
        };
    }
}
