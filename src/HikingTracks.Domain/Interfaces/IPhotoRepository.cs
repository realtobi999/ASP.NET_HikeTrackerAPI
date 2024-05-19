namespace HikingTracks.Domain;

public interface IPhotoRepository
{
    void CreatePhoto(Photo photo);
    void DeletePhoto(Photo photo);
    Task<Photo?> GetPhoto(Guid Id); 
}
