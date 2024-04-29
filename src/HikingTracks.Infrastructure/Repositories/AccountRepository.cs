using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly HikingTracksContext _context; 

    public AccountRepository(HikingTracksContext context)
    {
        _context = context;
    }

    public Account? GetAccount(Guid id)
    {
        return _context.Accounts.SingleOrDefault(account => account.ID == id);
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _context.Accounts.ToList();
    }
}
