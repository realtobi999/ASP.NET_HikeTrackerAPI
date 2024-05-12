namespace HikingTracks.GPXService;

public class Coordinate
{
    public double Latitude { get; }
    public double Longitude { get; }

    public Coordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        
        Validate();
    }

    private void Validate()
    {
        if (Latitude < -90 || Latitude > 90)
        {
            throw new InvalidCoordinateException("Latitude must be between -90 and 90 degrees.");
        }

        if (Longitude < -180 || Longitude > 180)
        {
            throw new InvalidCoordinateException("Longitude must be between -180 and 180 degrees.");
        }
    }

    public override string ToString()
    {
        return string.Format("{0}|{1}", Latitude, Longitude);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Coordinate other = (Coordinate)obj;
        return Latitude == other.Latitude && Longitude == other.Longitude;
    }

    public static Coordinate Parse(string coordinate)
    {
        var coordinates = coordinate.Split('|');
        if (coordinates.Length != 2)
        {
            throw new InvalidCoordinateException("Invalid coordinate format. Expected format: 'latitude|longitude'");
        }

        if (!double.TryParse(coordinates[0], out double latitude) || !double.TryParse(coordinates[1], out double longitude))
        {
            throw new InvalidCoordinateException("Invalid latitude or longitude format.");
        }

        return new Coordinate(latitude, longitude);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
    }
}

