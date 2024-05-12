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
    private readonly Lazy<ITokenService> _tokenService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, ITokenService tokenService)
    {
        _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager, logger));
        _hikeService = new Lazy<IHikeService>(() => new HikeService(repositoryManager, logger));
        _photoService = new Lazy<IPhotoService>(() => new PhotoService(repositoryManager, logger));
        _formFileService = new Lazy<IFormFileService>(() => new FormFileService());

        // This token service is already injected as a singleton due to jwt key and issuer being 
        // defaulted from appsettings.json
        _tokenService = new Lazy<ITokenService>(() => tokenService);
    }

    public IAccountService AccountService => _accountService.Value;
    public IHikeService HikeService => _hikeService.Value;
    public IPhotoService PhotoService => _photoService.Value;
    public IFormFileService FormFileService => _formFileService.Value;
    public ITokenService TokenService => _tokenService.Value;
}
