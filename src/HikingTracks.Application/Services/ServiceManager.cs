using HikingTracks.Application.Interfaces;
using HikingTracks.Application.Services.Account;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAccountService> _accountService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager));
    }

    public IAccountService AccountService => _accountService.Value;
}
