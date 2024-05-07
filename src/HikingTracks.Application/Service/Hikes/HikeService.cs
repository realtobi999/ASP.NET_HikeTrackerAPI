﻿using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application.Service.Hikes;

public class HikeService : IHikeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public HikeService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Hike> CreateHike(Guid accountId, CreateHikeDto createHikeDto)
    {
        var account = await _repository.Account.GetAccount(accountId) ?? throw new AccountNotFoundException(accountId); 

        var hike = new Hike{
            ID = createHikeDto.ID ?? Guid.NewGuid(),
            accountId = account.ID,
            Title = createHikeDto.Title,
            Description = createHikeDto.Description,
            Distance = createHikeDto.Distance,
            ElevationGain = createHikeDto.ElevationGain,
            ElevationLoss = createHikeDto.ElevationLoss,
            AverageSpeed = createHikeDto.Distance / createHikeDto.MovingTime.TotalSeconds,
            MaxSpeed = createHikeDto.MaxSpeed,
            MovingTime = createHikeDto.MovingTime,
            Coordinates = createHikeDto.Coordinates,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _repository.Hike.CreateHike(hike);

        // Update user total distance etc..
        account.UpdateAccountStatistics(hike); 

        await _repository.SaveAsync();

        return hike;
    }

    public async Task DeleteHike(Guid Id)
    {
        var hike = await _repository.Hike.GetHike(Id) ?? throw new HikeNotFoundException(Id);

        _repository.Hike.DeleteHike(hike);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<Hike>> GetAllHikes()
    {
        var hikes = await _repository.Hike.GetAllHikes();

        return hikes;        
    }

    public async Task<IEnumerable<Hike>> GetAllHikesByAccount(Guid accountId)
    {
        var hikes = await _repository.Hike.GetAllHikesByAccount(accountId);

        return hikes;
    }

    public async Task<Hike> GetHike(Guid Id)
    {
        var hike = await _repository.Hike.GetHike(Id) ?? throw new HikeNotFoundException(Id);

        return hike;
    }
}