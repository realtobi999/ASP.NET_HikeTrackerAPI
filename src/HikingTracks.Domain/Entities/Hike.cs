using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HikingTracks.Domain.DTO;

namespace HikingTracks.Domain.Entities;

public class Hike
{
    [Required, Column("id"), Key]
    public Guid ID { get; set; }

    [Required, Column("account_id")]
    public Guid AccountId { get; set; }

    [Required, Column("title")]
    public string? Title { get; set; }

    [Required, Column("description")]
    public string? Description { get; set; }

    [Required, Column("distance"), Range(0, double.MaxValue)]
    public double Distance { get; set; }

    [Required, Column("elevation_gain"), Range(0, double.MaxValue)]
    public double ElevationGain { get; set; }

    [Required, Column("elevation_loss"), Range(0, double.MaxValue)]
    public double ElevationLoss { get; set; }

    [Required, Column("average_speed"), Range(0, double.MaxValue)]
    public double AverageSpeed { get; set; }

    [Required, Column("max_speed"), Range(0, double.MaxValue)]
    public double MaxSpeed { get; set; }

    [Required, Column("moving_time")]
    public TimeSpan MovingTime { get; set; }

    [Required, Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [Required, Column("coordinates"), JsonIgnore]
    public string CoordinatesString
    {
        get => string.Join(";", Coordinates.Select(a => a.ToString()));
        set => Coordinates = value.Split(';').Select(Coordinate.Parse).ToList();
    }

    [NotMapped]
    public List<Coordinate> Coordinates = [];

    public Account? Account { get; set; }

    public ICollection<Photo> Photos { get; set; } = [];
    public ICollection<SegmentHike> SegmentHike { get; set; } = [];

    public HikeDto ToDTO()
    {
        var segments = this.SegmentHike
            .Where(segmentHike => segmentHike.Segment != null)
            .Select(segmentHike => segmentHike.Segment?.ToDTO())
            .ToList();

        return new HikeDto
        {
            ID = this.ID,
            AccountId = this.AccountId,
            Distance = this.Distance,
            ElevationGain = this.ElevationGain,
            ElevationLoss = this.ElevationLoss,
            AverageSpeed = this.AverageSpeed,
            MaxSpeed = this.MaxSpeed,
            MovingTime = this.MovingTime,
            Coordinates = this.Coordinates ?? new List<Coordinate>(),
            Photos = [.. this.Photos],
            Segments = segments!,
            CreatedAt = this.CreatedAt
        };
    }

}
