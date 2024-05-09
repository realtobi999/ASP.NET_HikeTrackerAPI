﻿namespace HikingTracks.Domain.Interfaces;

public interface IRepositoryManager
{
    IAccountRepository Account { get; }
    IHikeRepository Hike { get; }
    IPhotoRepository Photo { get; }
    Task<int> SaveAsync();
}
