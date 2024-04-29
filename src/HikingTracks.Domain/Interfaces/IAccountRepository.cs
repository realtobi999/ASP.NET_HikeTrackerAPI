using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface IAccountRepository
{
    IEnumerable<Account> GetAllAccounts();
    Account? GetAccount(Guid id);
    void CreateAccount(Account account); 
}
