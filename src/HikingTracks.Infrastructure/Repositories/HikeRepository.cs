using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure.Repositories;

public class HikeRepository : IHikeRepository
{
    private readonly HikingTracksContext _context;

    public HikeRepository(HikingTracksContext context)
    {
        _context = context;
    }

    public void CreateHike(Hike hike)
    {
        _context.Hikes.Add(hike);
    }

    public void DeleteHike(Hike hike)
    {
        _context.Hikes.Remove(hike);
    }

    public async Task<IEnumerable<Hike>> GetAllHikes()
    {
        return await _context.Hikes
            .Include(h => h.Photos)
            .Include(h => h.SegmentHike)
                .ThenInclude(sh => sh.Segment)
            .OrderBy(hike => hike.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Hike>> GetAllHikesByAccount(Guid accountId)
    {
        return await _context.Hikes
            .Include(h => h.Photos)
            .Include(h => h.SegmentHike)
                .ThenInclude(sh => sh.Segment)
            .Where(hike => hike.AccountId == accountId)
            .ToListAsync();
    }

    public async Task<Hike?> GetHike(Guid id)
    {
        return await _context.Hikes
            .Include(h => h.Photos)
            .Include(h => h.SegmentHike)
                .ThenInclude(sh => sh.Segment)
            .SingleOrDefaultAsync(h => h.ID == id);
    }

}
