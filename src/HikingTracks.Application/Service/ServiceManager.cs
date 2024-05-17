using HikingTracks.Application.Interfaces;
namespace HikingTracks.Application.Service;

public class ServiceManager : IServiceManager
{
    private readonly IServiceFactory _factory;
    public ServiceManager(IServiceFactory factory)
    {
        _factory = factory;
    }

    public IAccountService AccountService => _factory.CreateAccountService();
    public IHikeService HikeService => _factory.CreateHikeService();
    public IPhotoService PhotoService => _factory.CreatePhotoService();
    public IFormFileService FormFileService => _factory.CreateFormFileService();
    public ITokenService TokenService => _factory.CreateTokenService();
    public ISegmentService SegmentService => _factory.CreateSegmentService();
    public ISegmentHikeService SegmentHikeService => _factory.CreateSegmentHikeService();
}
