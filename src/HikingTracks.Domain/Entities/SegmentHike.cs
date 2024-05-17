using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Entities;

public class SegmentHike
{
    public Guid SegmentId { get; set; }
    public Segment? Segment { get; set; }

    public Guid HikeId { get; set; }
    public Hike? Hike { get; set; }
}
