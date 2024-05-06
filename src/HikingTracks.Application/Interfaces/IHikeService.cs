using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface IHikeService
{
    Task<IEnumerable<Hike>> GetAllHikes();
    Task<IEnumerable<Hike>> GetAllHikesByAccount(Guid accountId);
    Task<Hike> GetHike(Guid Id);
    Task<Hike> CreateHike(Guid accountId, CreateHikeDto createHikeDto);
    Task DeleteHike(Guid Id);
}
