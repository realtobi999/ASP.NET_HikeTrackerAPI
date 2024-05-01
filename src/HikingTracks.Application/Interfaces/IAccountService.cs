using HikingTracks.Domain;
using HikingTracks.Domain.DTO;

namespace HikingTracks.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountDto>> GetAllAccounts();
    Task<AccountDto> GetAccount(Guid id);
    Task<AccountDto> CreateAccount(CreateAccountDto createAccountDto);
    Task<int> UpdateAccount(Guid id, UpdateAccountDto updateAccountDto);  
}
