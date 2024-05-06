using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application.Service.Accounts;

public class AccountService : IAccountService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public AccountService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Account> CreateAccount(CreateAccountDto createAccountDto)
    {
        var account = new Account(){
            ID = createAccountDto.ID ?? Guid.NewGuid(),
            Username = createAccountDto.Username,
            Email = createAccountDto.Email,
            Password = createAccountDto.Password,
            Token = Guid.NewGuid(),
            TotalHikes = 0,
            TotalDistance = 0.00,
            TotalMovingTime = TimeSpan.Zero,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _repository.Account.CreateAccount(account);
        await _repository.SaveAsync();

        return account;
    }

    public async Task DeleteAccount(Guid id)
    {
        var account = await _repository.Account.GetAccount(id) ?? throw new AccountNotFoundException(id);

        _repository.Account.DeleteAccount(account);
        await _repository.SaveAsync();
    }

    public async Task<Account> GetAccount(Guid id)
    {
        var account = await _repository.Account.GetAccount(id) ?? throw new AccountNotFoundException(id);
        return account;
    }

    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        var accounts = await _repository.Account.GetAllAccounts();

        return accounts;
    }

    public async Task<int> UpdateAccount(Guid id, UpdateAccountDto updateAccountDto)
    {
        var account = await _repository.Account.GetAccount(id) ?? throw new AccountNotFoundException(id);

        account.Username = updateAccountDto.Username;
        account.Email = updateAccountDto.Email;
        account.TotalHikes = updateAccountDto.TotalHikes;
        account.TotalDistance = updateAccountDto.TotalDistance;
        account.TotalMovingTime = updateAccountDto.TotalMovingTime;

        return await _repository.SaveAsync();
    }
}
