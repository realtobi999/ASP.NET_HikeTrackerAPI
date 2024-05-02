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

    public async Task<IEnumerable<Hike>> GetAllHikes()
    {
        return await _context.Hikes.ToListAsync();
    }

    public async Task<Hike?> GetHike(Guid Id)
    {
        return await _context.Hikes.SingleOrDefaultAsync(account => account.ID == Id);
    }
}
