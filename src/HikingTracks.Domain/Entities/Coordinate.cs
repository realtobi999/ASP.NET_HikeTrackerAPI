﻿using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Domain.Entities;

public class Coordinate
{
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

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude, Elevation);
    }

    public static Coordinate Parse(string coordinate)
    {
        var coordinates = coordinate.Split('|');
        if (coordinates.Length != 3)
        {
            throw new InvalidCoordinateException("Invalid coordinate format. Expected format: 'latitude|longitude|elevation'");
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


