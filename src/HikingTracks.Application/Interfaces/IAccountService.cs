using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> GetAccount(Guid id);
    Task<Account> CreateAccount(CreateAccountDto createAccountDto);
    Task<int> UpdateAccount(Guid id, UpdateAccountDto updateAccountDto);  
    Task DeleteAccount(Guid id); 
}
