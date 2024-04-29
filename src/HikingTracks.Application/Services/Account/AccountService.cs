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

    /// <summary>
    /// Retrieves an account with the specified ID from the repository.
    /// </summary>
    /// <returns>
    /// AccountDto representing the retrieved account, or null if the account is not found.
    /// </returns>
    public AccountDto? GetAccount(Guid id)
    {
        var account = _repository.Account.GetAccount(id);

        if (account is null) 
        {
            return null;
        }

        return account.ToDTO();
    }

    /// <summary>
    /// Retrieves all accounts from the repository and converts them to AccountDto objects.
    /// </summary>
    /// <returns>A collection of AccountDto representing all accounts.</returns>
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
