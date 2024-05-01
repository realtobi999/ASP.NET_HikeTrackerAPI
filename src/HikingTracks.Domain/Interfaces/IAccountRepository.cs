using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account?> GetAccount(Guid id);
    
    void CreateAccount(Account account); 
}
