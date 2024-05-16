using HikingTracks.Domain;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly IRepositoryFactory _factory;
    private readonly HikingTracksContext _context;

    public RepositoryManager(IRepositoryFactory factory, HikingTracksContext context)
    {
        _factory = factory;
        _context = context;
    }

    public IAccountRepository Account => _factory.CreateAccountRepository();
    public IHikeRepository Hike => _factory.CreateHikeRepository(); 
    public IPhotoRepository Photo => _factory.CreatePhotoRepository(); 
    public ISegmentRepository Segment => _factory.CreateSegmentRepository(); 
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
