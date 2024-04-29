using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly HikingTracksContext _context;
    private readonly Lazy<IAccountRepository> _accountRepository;

    public RepositoryManager(HikingTracksContext context)
    {
        _context = context;
        _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(_context));   
    }

    public IAccountRepository Account => _accountRepository.Value;

    public void Save()
    {
        _context.SaveChanges();
    }
}
