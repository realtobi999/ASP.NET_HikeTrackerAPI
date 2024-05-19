using HikingTracks.Domain;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure;

public class PhotoRepository : IPhotoRepository
{
    private readonly HikingTracksContext _context;

    public PhotoRepository(HikingTracksContext context)
    {
        _context = context;
    }

    public void CreatePhoto(Photo photo)
    {
        _context.Photos.Add(photo);
    }

    public void DeletePhoto(Photo photo)
    {
        _context.Photos.Remove(photo);
    }

    public async Task<Photo?> GetPhoto(Guid Id)
    {
        return await _context.Photos.SingleOrDefaultAsync(photo => photo.ID == Id);
    }
}
