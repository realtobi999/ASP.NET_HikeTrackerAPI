using FluentAssertions;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Tests.Unit;

public class CoordinateTests
{
    // Test for coordinate validation
    [Fact]
    public void Coordinate_Validation_Works()
    {
        // Prepare & Act
        var coordinate1 = new Coordinate(-90, -180, 0); // Valid coordinates
        var coordinate2 = new Coordinate(90, 180, 100); // Valid coordinates

        // Assert
        Assert.Throws<InvalidCoordinateException>(() => new Coordinate(91, 0, 0))
            .Message.Should().Be("Latitude must be between -90 and 90 degrees."); // Invalid latitude
        Assert.Throws<InvalidCoordinateException>(() => new Coordinate(0, 181, 0))
            .Message.Should().Be("Longitude must be between -180 and 180 degrees."); // Invalid longitude
    }

    // Test for ToString method
    [Fact]
    public void Coordinate_ToString_Works()
    {
        // Prepare
        var coordinate = new Coordinate(90, 180, 100);

        // Act & Assert
        coordinate.ToString().Should().Be("90|180|100"); // Expected output
    }

    // Test for Equals method
    [Fact]
    public void Coordinate_Equals_Works()
    {
        // Prepare
        var coordinate1 = new Coordinate(90, 180, 100);
        var coordinate2 = new Coordinate(90, 179, 100);
        var coordinate3 = new Coordinate(90, 180, 100);
        var coordinate4 = new Coordinate(-90, 180, 100);
        var coordinate5 = new Coordinate(90, -180, 100);

        // Act & Assert
        coordinate1.Equals(coordinate2).Should().Be(false); // Different longitude
        coordinate3.Equals(coordinate4).Should().Be(false); // Different latitude
        coordinate1.Equals(coordinate3).Should().Be(true); // Same coordinates
        coordinate4.Equals(coordinate5).Should().Be(false); // Different latitude and longitude
    }

    // Test for Parse method
    [Fact]
    public void Coordinate_Parse_Works()
    {
        // Prepare & Act
        var coordinate1 = Coordinate.Parse("90|132|100");

        // Assert
        coordinate1.Latitude.Should().Be(90);
        coordinate1.Longitude.Should().Be(132);
        coordinate1.Elevation.Should().Be(100);

        // Invalid coordinate formats
        Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("invalid")); // No separators
        Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("90|invalid|100")); // Invalid longitude
        Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("invalid|132|100")); // Invalid latitude
        Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("90|30|34|100")); // Extra elevation value
        Assert.Throws<InvalidCoordinateException>(() => Coordinate.Parse("90;34;100")); // Invalid separator
    }

    [Fact]
    public void Coordinate_IsWithinRange_Works()
    {
        // Prepare
        var coordinate = new Coordinate(50, 100, 0);

        // Act & Assert
        Assert.True(coordinate.IsWithinRange(new Coordinate(50, 100, 0), 1));
        Assert.True(coordinate.IsWithinRange(new Coordinate(50.05, 100.05, 0), 7000)); // Approximately 7km away 
        Assert.True(coordinate.IsWithinRange(new Coordinate(50.005, 100.005, 0), 1000)); // Approximately 1km away
        Assert.False(coordinate.IsWithinRange(new Coordinate(55, 105, 0), 1)); // Approximately 650km away
   
        Assert.Throws<ArgumentException>(() => coordinate.IsWithinRange(new Coordinate(0,0,0), -1))
            .Message.Should().Be("IsWithinRange 'range' argument needs to be bigger than zero.");
    }
}
