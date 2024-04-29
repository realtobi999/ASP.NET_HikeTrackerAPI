using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface IAccountRepository
{
    IEnumerable<Account> GetAllAccounts();
}
