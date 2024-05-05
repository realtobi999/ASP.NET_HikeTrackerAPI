using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface IHikeService
{
    Task<IEnumerable<Hike>> GetAllHikes(int limit = 0, int offset = 0);
    Task<Hike> GetHike(Guid Id);
    Task<Hike> CreateHike(Guid accountID, CreateHikeDto createHikeDto);
    Task DeleteHike(Guid Id);
}
