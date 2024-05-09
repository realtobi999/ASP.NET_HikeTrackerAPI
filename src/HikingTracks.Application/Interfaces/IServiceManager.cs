using HikingTracks.Application.Interfaces;

namespace HikingTracks.Application.Interfaces;

public interface IServiceManager
{
    public IAccountService AccountService { get; }
    public IHikeService HikeService { get; }
    public IPhotoService PhotoService { get; }
    public IFormFileService FormFileService { get; }
}
