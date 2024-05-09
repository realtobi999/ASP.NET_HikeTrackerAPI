using HikingTracks.Domain;

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
}
