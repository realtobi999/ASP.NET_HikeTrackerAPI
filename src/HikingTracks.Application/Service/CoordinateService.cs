using HikingTracks.Domain.Entities;

namespace HikingTracks.Application;

public class CoordinateService : ICoordinateService
{
    public static IEnumerable<Coordinate> GenerateRandomCoordinatesList(int Length)
    {
        var random = new Random();
        var coordinates = new List<Coordinate>();

        for (int i = 0; i < Length; i++)
        {
            var latitude = random.NextDouble() * (90 - (-90)) + (-90);
            var longitude = random.NextDouble() * (180 - (-180)) + (-180);
            var elevation = random.NextDouble();
            coordinates.Add(new Coordinate(latitude, longitude,elevation));
        }

        return coordinates;
    }
}
