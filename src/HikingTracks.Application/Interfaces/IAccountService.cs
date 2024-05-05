using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccounts(int limit = 0, int offset = 0);
    Task<Account> GetAccount(Guid id);
    Task<Account> CreateAccount(CreateAccountDto createAccountDto);
    Task<int> UpdateAccount(Guid id, UpdateAccountDto updateAccountDto);  
    Task DeleteAccount(Guid id); 
}
