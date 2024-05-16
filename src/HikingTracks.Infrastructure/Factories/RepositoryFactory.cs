using HikingTracks.Domain;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;
using HikingTracks.Infrastructure.Repositories;

namespace HikingTracks.Infrastructure;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly HikingTracksContext _context;

    public RepositoryFactory(HikingTracksContext context)
    {
        _context = context;
    }

    public IAccountRepository CreateAccountRepository()
    {
        return new AccountRepository(_context);
    }

    public IHikeRepository CreateHikeRepository()
    {
        return new HikeRepository(_context);
    }

    public IPhotoRepository CreatePhotoRepository()
    {
        return new PhotoRepository(_context);
    }

    public ISegmentRepository CreateSegmentRepository()
    {
        return new SegmentRepository(_context);
    }
}
