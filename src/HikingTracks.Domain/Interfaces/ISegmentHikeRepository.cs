using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface ISegmentHikeRepository
{
    void CreateSegmentHike(SegmentHike segmentHike);
}
