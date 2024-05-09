using Microsoft.AspNetCore.Http;

namespace HikingTracks.Application;

public interface IFormFileService
{
    Task<byte[]> IntoByteArray(IFormFile file);
}
