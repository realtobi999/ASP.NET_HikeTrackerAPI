﻿using HikingTracks.Domain;
using HikingTracks.Domain.DTO;

namespace HikingTracks.Application.Interfaces;

public interface IAccountService
{
    IEnumerable<AccountDto> GetAllAccounts();
    AccountDto GetAccount(Guid id);
    AccountDto CreateAccount(CreateAccountDto createAccountDto);
}
