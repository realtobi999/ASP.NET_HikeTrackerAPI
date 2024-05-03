using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HikingTracks.Domain.Entities;

public class Hike
{
    [Required, Column("id"), Key]
    public Guid ID { get; set; }         
    
    [Required, Column("account_id")]
    public Guid AccountID { get; set; }

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

    [Required, Column("coordinates")]
    public string CoordinatesString
    {
        get => string.Join(";", Coordinates.Select(a => a.ToString()));
        set => Coordinates = value.Split(';').Select(Coordinate.Parse).ToList();
    }

    [NotMapped]
    public List<Coordinate> Coordinates = new(); 

    [ForeignKey("account_id")]
    public Account? Account { get; set; }

    public HikeDto ToDTO()
    {
        return new HikeDto
        {
            ID = ID,
            AccountID = AccountID,
            Distance = Distance,
            ElevationGain = ElevationGain,
            ElevationLoss = ElevationLoss,
            AverageSpeed = AverageSpeed,
            MaxSpeed = MaxSpeed,
            MovingTime = MovingTime,
            Coordinates = Coordinates ?? []
        };
    }
}
