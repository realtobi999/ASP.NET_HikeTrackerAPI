using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Domain;

public interface IRepositoryFactory
{
    IAccountRepository CreateAccountRepository();
    IHikeRepository CreateHikeRepository();
    IPhotoRepository CreatePhotoRepository();
    ISegmentRepository CreateSegmentRepository();
    ISegmentHikeRepository CreateSegmentHikeRepository();
}
