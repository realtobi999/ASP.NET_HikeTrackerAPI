using HikingTracks.Domain;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure.Repositories;

public class SegmentRepository : ISegmentRepository
{
    private readonly HikingTracksContext _context;

    public SegmentRepository(HikingTracksContext context)
    {
        _context = context;
    }

    public void CreateSegment(Segment segment)
    {
        _context.Segments.Add(segment);
    }

    public async Task<IEnumerable<Segment>> GetAllSegments()
    {
        return await _context.Segments.OrderBy(segment => segment.CreatedAt).ToListAsync();
    }

    public async Task<Segment?> GetSegment(Guid id)
    {
        return await _context.Segments.SingleOrDefaultAsync(segment => segment.ID == id);
    }
}
