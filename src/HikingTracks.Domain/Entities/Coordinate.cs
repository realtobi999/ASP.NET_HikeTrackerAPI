using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Domain.Entities;

public class Coordinate
{
    public const string ValidCoordinateFormat = "latitude|longitude|elevation";
    public double Latitude { get; }
    public double Longitude { get; }
    public double Elevation { get; }

    public Coordinate(double latitude, double longitude, double elevation)
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new InvalidCoordinateException("Latitude must be between -90 and 90 degrees.");
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new InvalidCoordinateException("Longitude must be between -180 and 180 degrees.");
        }

        Latitude = latitude;
        Longitude = longitude;
        Elevation = elevation;
    }

    public override string ToString()
    {
        return $"{Latitude}|{Longitude}|{Elevation}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Coordinate other = (Coordinate)obj;
        return Latitude == other.Latitude && Longitude == other.Longitude && Elevation == other.Elevation;
    }

    public bool IsWithinRange(Coordinate coordinate, int range)
    {
        if (range <= 0)
            throw new ArgumentException("IsWithinRange 'range' argument needs to be bigger than zero.");
        
        var distance = CoordinateMath.Haversine(this.Latitude.ToRadians(), coordinate.Latitude.ToRadians(), this.Longitude.ToRadians(), coordinate.Longitude.ToRadians());

        return distance <= range;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude, Elevation);
    }

    public static Coordinate Parse(string coordinate)
    {
        var coordinates = coordinate.Split('|');
        if (coordinates.Length != 3)
        {
            throw new InvalidCoordinateException(string.Format("Invalid coordinate format. Expected format: '{0}'", ValidCoordinateFormat));
        }

        if (!double.TryParse(coordinates[0], out double latitude) ||
            !double.TryParse(coordinates[1], out double longitude) ||
            !double.TryParse(coordinates[2], out double elevation))
        {
            throw new InvalidCoordinateException("Invalid latitude, longitude, or elevation format.");
        }

        return new Coordinate(latitude, longitude, elevation);
    }
}


