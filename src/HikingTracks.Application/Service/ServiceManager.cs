using HikingTracks.Application.Interfaces;
namespace HikingTracks.Application.Service;

public class ServiceManager : IServiceManager
{
    private readonly IServiceFactory _serviceFactory;
    public ServiceManager(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public IAccountService AccountService => _serviceFactory.CreateAccountService();
    public IHikeService HikeService => _serviceFactory.CreateHikeService();
    public IPhotoService PhotoService => _serviceFactory.CreatePhotoService();
    public IFormFileService FormFileService => _serviceFactory.CreateFormFileService();
    public ITokenService TokenService => _serviceFactory.CreateTokenService();
    public ISegmentService SegmentService => _serviceFactory.CreateSegmentService();
}
