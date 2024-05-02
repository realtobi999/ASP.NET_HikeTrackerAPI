using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface IHikeRepository
{
    Task<IEnumerable<Hike>> GetAllHikes();
    void CreateHike(Hike hike);
}
