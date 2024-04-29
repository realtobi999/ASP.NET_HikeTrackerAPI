namespace HikingTracks.Domain.Interfaces;

public interface IRepositoryManager
{
    IAccountRepository Account { get; }
    void Save();
}
