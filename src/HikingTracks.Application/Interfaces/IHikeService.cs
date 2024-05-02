using HikingTracks.Domain;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface IHikeService
{
    Task<IEnumerable<Hike>> GetAllHikes();
    Task<Hike> CreateHike(Guid accountID, CreateHikeDto createHikeDto);
}
