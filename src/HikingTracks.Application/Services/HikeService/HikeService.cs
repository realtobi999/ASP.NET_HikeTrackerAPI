﻿using HikingTracks.Application.Interfaces;
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

    public async Task<Hike> CreateHike(Guid accountID, CreateHikeDto createHikeDto)
    {
        var account = await _repository.Account.GetAccount(accountID) ?? throw new AccountNotFoundException(accountID); 

        var hike = new Hike{
            ID = Guid.NewGuid(),
            AccountID = account.ID,
            Distance = createHikeDto.Distance,
            ElevationGain = createHikeDto.ElevationGain,
            ElevationLoss = createHikeDto.ElevationLoss,
            AverageSpeed = createHikeDto.Distance / createHikeDto.MovingTime.TotalSeconds,
            MaxSpeed = createHikeDto.MaxSpeed,
            MovingTime = createHikeDto.MovingTime,
            Coordinates = createHikeDto.Coordinates,
        };

        _repository.Hike.CreateHike(hike);
        
        account.TotalHikes++;
        account.TotalDistance += hike.Distance;
        account.TotalMovingTime += hike.MovingTime;

        await _repository.SaveAsync();

        return hike;
    }

    public async Task<IEnumerable<Hike>> GetAllHikes()
    {
        var hikes = await _repository.Hike.GetAllHikes();
        return hikes;        
    }

    public async Task<Hike> GetHike(Guid Id)
    {
        var hike = await _repository.Hike.GetHike(Id) ?? throw new HikeNotFoundException(Id);

        return hike;
    }
}
