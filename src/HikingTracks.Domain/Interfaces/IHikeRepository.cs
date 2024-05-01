using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.Interfaces;

public interface IHikeRepository
{
    void CreateHike(Hike hike);
}
