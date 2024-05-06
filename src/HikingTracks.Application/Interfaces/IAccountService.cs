using System.Security.Claims;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> GetAccount(Guid Id);
    Task<Account> LoginAccount(LoginAccountDto loginAccountDto);    
    Task<Account> CreateAccount(CreateAccountDto createAccountDto);
    Task<int> UpdateAccount(Guid Id, UpdateAccountDto updateAccountDto);  
    Task DeleteAccount(Guid Id); 
}
