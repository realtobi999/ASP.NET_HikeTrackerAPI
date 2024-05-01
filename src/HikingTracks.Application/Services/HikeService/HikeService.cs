using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application.Services.HikeService;

public class HikeService : IHikeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public HikeService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<HikeDto> CreateHike(Guid accountID, CreateHikeDto createHikeDto)
    {
        var account = await _repository.Account.GetAccount(accountID) ?? throw new AccountNotFoundException(accountID); 

        var hike = new Hike{
            ID = Guid.NewGuid(),
            AccountID = account.ID,
            Distance = createHikeDto.Distance,
            ElevationGain = createHikeDto.ElevationGain,
            ElevationLoss = createHikeDto.ElevationLoss,
            AverageSpeed = 0,
            MaxSpeed = createHikeDto.MaxSpeed,
            MovingTime = createHikeDto.MovingTime,
            CoordinatesString = createHikeDto.CoordinatesString,
        };

        _repository.Hike.CreateHike(hike);
        await _repository.SaveAsync();

        return hike.ToDTO();
    }
}
