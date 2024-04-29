using HikingTracks.Application.Interfaces;
using HikingTracks.Application.Services.AccountService;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAccountService> _accountService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
    {
        _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager, logger));
    }

    public IAccountService AccountService => _accountService.Value;
}
