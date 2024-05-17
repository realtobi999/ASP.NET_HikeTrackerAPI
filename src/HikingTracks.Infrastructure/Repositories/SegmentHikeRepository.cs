using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Infrastructure;

public class SegmentHikeRepository : ISegmentHikeRepository
{
    private readonly HikingTracksContext _context;

    public SegmentHikeRepository(HikingTracksContext context)
    {
        _context = context;
    }

    public void CreateSegmentHike(SegmentHike segmentHike)
    {
        _context.SegmentHike.Add(segmentHike);
    }
}
