using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface ISegmentHikeService
{
    Task<SegmentHike> CreateSegmentHike(Segment segment, Hike hike);
}
