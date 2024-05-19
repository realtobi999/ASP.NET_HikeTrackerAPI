namespace HikingTracks.Domain.DTO;

public record class UpdateHikeDto
{
    public double Distance { get; set; }
    public double ElevationGain { get; set; }
    public double ElevationLoss { get; set; }
    public double MaxSpeed { get; set; }
    public TimeSpan MovingTime { get; set; }
    public int Kudos { get; set; }
    public List<Photo> Photos { get; set; } = [];
}
