namespace HikingTracks.Domain.Interfaces;

public interface IRepositoryManager
{
    IAccountRepository Account { get; }
    IHikeRepository Hike { get; }
    Task<int> SaveAsync();
}
