using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface ISegmentRepository
{
    Task<IEnumerable<Segment>> GetAllSegments();
    Task<Segment?> GetSegment(Guid id);
    void CreateSegment(Segment segment);
}
