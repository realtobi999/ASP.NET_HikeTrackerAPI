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
        return await _context.Hikes.OrderBy(hike => hike.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Hike>> GetAllHikesByAccount(Guid accountId)
    {
        return await _context.Hikes.Where(hike => hike.accountId == accountId).ToListAsync();
    }

    public async Task<Hike?> GetHike(Guid Id)
    {
        return await _context.Hikes.SingleOrDefaultAsync(account => account.ID == Id);
    }
}
