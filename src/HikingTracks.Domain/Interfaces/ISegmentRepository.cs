using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface ISegmentRepository
{
    Task<IEnumerable<Segment>> GetAllSegments();
    void CreateSegment(Segment segment);
}
