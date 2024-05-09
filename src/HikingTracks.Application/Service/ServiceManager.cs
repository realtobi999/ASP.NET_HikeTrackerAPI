using HikingTracks.Application.Interfaces;
using HikingTracks.Application.Service.Accounts;
using HikingTracks.Application.Service.Hikes;
using HikingTracks.Application.Service.Photos;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAccountService> _accountService;
    private readonly Lazy<IHikeService> _hikeService;
    private readonly Lazy<IPhotoService> _photoService;
    private readonly Lazy<IFormFileService> _formFileService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
    {
        _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager, logger));
        _hikeService = new Lazy<IHikeService>(() => new HikeService(repositoryManager, logger));
        _photoService = new Lazy<IPhotoService>(() => new PhotoService(repositoryManager, logger));
        _formFileService = new Lazy<IFormFileService>(() => new FormFileService());
    }

    public IAccountService AccountService => _accountService.Value;
    public IHikeService HikeService => _hikeService.Value;
    public IPhotoService PhotoService => _photoService.Value;
    public IFormFileService FormFileService => _formFileService.Value;
}
