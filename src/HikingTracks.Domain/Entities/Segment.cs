using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HikingTracks.Domain.DTO;

namespace HikingTracks.Domain.Entities;

public class Segment
{
    [Required]
    public Guid ID { get; set; }

    [Required, Column("name")]
    public string? Name { get; set; }

    [Required, Column("distance"), Range(0, double.MaxValue)]
    public double Distance { get; set; }

    [Required, Column("elevation_gain"), Range(0, double.MaxValue)]
    public double ElevationGain { get; set; }

    [Required, Column("elevation_loss"), Range(0, double.MaxValue)]
    public double ElevationLoss { get; set; }

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

    [JsonIgnore]
    public ICollection<SegmentHike> SegmentHike { get; set; } = [];


    public SegmentDto ToDTO()
    {
        return new SegmentDto
        {
            ID = this.ID,
            Name = this.Name,
            Distance = this.Distance,
            ElevationGain = this.ElevationGain,
            ElevationLoss = this.ElevationLoss,
            Coordinates = this.Coordinates,
            CreatedAt = this.CreatedAt
        };
    }
}
