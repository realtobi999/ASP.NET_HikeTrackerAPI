using FluentAssertions;
using HikingTracks.Application.Services.AccountService;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;
using HikingTracks.Tests.Integration.AccountEndpointTests;
using Moq;

namespace HikingTracks.Tests.Unit.AccountServiceTests;

public class AccountServiceTests
{
    [Fact]
    public async void Account_GetAccount_FailsWhenNotFound()
    {
        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();
        repository.Setup(repo => repo.Account.GetAccount(Guid.Empty)).ReturnsAsync((Account)null);
        var service = new AccountService(repository.Object, logger.Object);

        var account = await Assert.ThrowsAsync<AccountNotFoundException>(async () => await service.GetAccount(Guid.Empty));
    } 
}
