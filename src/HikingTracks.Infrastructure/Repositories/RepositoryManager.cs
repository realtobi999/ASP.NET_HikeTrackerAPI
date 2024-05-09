using HikingTracks.Domain;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly HikingTracksContext _context;
    private readonly Lazy<IAccountRepository> _accountRepository;
    private readonly Lazy<IHikeRepository> _hikeRepository;
    private readonly Lazy<IPhotoRepository> _photoRepository;

    public RepositoryManager(HikingTracksContext context)
    {
        _context = context;
        _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(_context));   
        _hikeRepository = new Lazy<IHikeRepository>(() => new HikeRepository(_context));
        _photoRepository = new Lazy<IPhotoRepository>(() => new PhotoRepository(_context));
    }

    public IAccountRepository Account => _accountRepository.Value;
    public IHikeRepository Hike => _hikeRepository.Value;
    public IPhotoRepository Photo => _photoRepository.Value;

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
