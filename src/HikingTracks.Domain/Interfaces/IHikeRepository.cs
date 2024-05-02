using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface IHikeRepository
{
    Task<IEnumerable<Hike>> GetAllHikes();
    Task<Hike?> GetHike(Guid Id);
    void CreateHike(Hike hike);
}
