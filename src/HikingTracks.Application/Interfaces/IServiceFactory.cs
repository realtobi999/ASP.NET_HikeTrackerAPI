namespace HikingTracks.Application.Interfaces;

public interface IServiceFactory
{
    IAccountService CreateAccountService();
    IHikeService CreateHikeService();
    IPhotoService CreatePhotoService();
    IFormFileService CreateFormFileService();
    ISegmentService CreateSegmentService();
    ITokenService CreateTokenService();
}