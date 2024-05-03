using FluentAssertions;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Tests.Unit;

public class CoordinateTests
{
    [Fact]
    public void Coordinate_Validation_Works()
    {
        var coordinate1 = new Coordinate(-90, -180); // Works
        var coordinate2 = new Coordinate(90, 180); // Works

        var coordinateException1 = Assert.Throws<InvalidCoordinateException>(() => new Coordinate(91, 0));
        coordinateException1.Message.Should().Be("Latitude must be between -90 and 90 degrees.");

        var coordinateException2 = Assert.Throws<InvalidCoordinateException>(() => new Coordinate(0, 181));
        coordinateException2.Message.Should().Be("Longitude must be between -180 and 180 degrees.");
    }

    [Fact]
    public void Coordinate_ToString_Works()
    {
        var coordinate = new Coordinate(90,180);

        (coordinate.ToString()).Should().Be("90|180");
    }

    [Fact]
    public void Coordinate_Equals_Works()
    {
        var coordinate1 = new Coordinate(90, 180);
        var coordinate2 = new Coordinate(90, 179);

        (coordinate1.Equals(coordinate2)).Should().Be(false);

        var coordinate3 = new Coordinate(90, 180);
        var coordinate4 = new Coordinate(90, 180);

        (coordinate3.Equals(coordinate4)).Should().Be(true);

        var coordinate5 = new Coordinate(-90, 180);
        var coordinate6 = new Coordinate(90, -180);

        (coordinate5.Equals(coordinate6)).Should().Be(false);
    }

    [Fact]
    public void Coordinate_Parse_Works()
    {
        var coordinate1 = Coordinate.Parse("90|132");

        coordinate1.Latitude.Should().Be(90);
        coordinate1.Longitude.Should().Be(132);

        _ = Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("invalid"));
        _ = Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("90|invalid"));
        _ = Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("invalid|132")); 
        _ = Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("90|30|34"));
        _ = Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("90;34"));
    }
}
