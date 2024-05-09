using HikingTracks.Domain;

namespace HikingTracks.Application;

public interface IPhotoService
{
    Task<Photo> CreatePhoto(CreatePhotoDto createPhotoDto);
}
