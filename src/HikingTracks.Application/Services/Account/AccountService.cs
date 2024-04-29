using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application.Services.Account;

public class AccountService : IAccountService
{
    private readonly IRepositoryManager _repository;

    public AccountService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public IEnumerable<AccountDto> GetAllAccounts()
    {
        var accounts = _repository.Account.GetAllAccounts();
        var accountsDto = new List<AccountDto>();

        foreach (var account in accounts) {
            accountsDto.Add(account.ToDTO());
        }

        return accountsDto;
    }
}
