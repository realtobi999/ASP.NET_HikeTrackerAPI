﻿using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public AccountService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public AccountDto CreateAccount(CreateAccountDto createAccountDto)
    {
        var account = new Account(){
            ID = Guid.NewGuid(),
            Username = createAccountDto.Username,
            Email = createAccountDto.Email,
            Password = createAccountDto.Password,
            Token = "test",
            TotalHikes = 0,
            TotalDistance = 0.00,
            TotalMovingTime = TimeSpan.Zero,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _repository.Account.CreateAccount(account);
        _repository.Save();

        return account.ToDTO();
    }

    public AccountDto GetAccount(Guid id)
    {
        var account = _repository.Account.GetAccount(id);

        if (account is null) 
        {
            throw new AccountNotFoundException(id);
        }

        return account.ToDTO();
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
