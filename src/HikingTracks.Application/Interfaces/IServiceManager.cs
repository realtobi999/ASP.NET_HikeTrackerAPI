using HikingTracks.Application.Interfaces;

namespace HikingTracks.Application.Interfaces;

public interface IServiceManager
{
    public IAccountService AccountService { get; }
    public IHikeService HikeService { get; }
}
