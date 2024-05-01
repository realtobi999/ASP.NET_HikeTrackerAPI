using HikingTracks.Domain;

namespace HikingTracks.Application.Interfaces;

public interface IHikeService
{
    Task<HikeDto> CreateHike(Guid accountID, CreateHikeDto createHikeDto);
}
