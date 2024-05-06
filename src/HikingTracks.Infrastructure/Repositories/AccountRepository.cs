using HikingTracks.Domain;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly HikingTracksContext _context; 

    public AccountRepository(HikingTracksContext context)
    {
        _context = context;
    }

    public void CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
    }

    public void DeleteAccount(Account account)
    {
        _context.Accounts.Remove(account);
    }

    public async Task<Account?> GetAccountByCredentials(string email, string password)
    {
        return await _context.Accounts.FirstOrDefaultAsync(account => account.Email == email && account.Password == password);
    }

    async Task<Account?> IAccountRepository.GetAccount(Guid id)
    {
        return await _context.Accounts.SingleOrDefaultAsync(account => account.ID == id);
    }

    async Task<IEnumerable<Account>> IAccountRepository.GetAllAccounts()
    {
        return await _context.Accounts.OrderBy(account => account.CreatedAt).ToListAsync();
    }
}
