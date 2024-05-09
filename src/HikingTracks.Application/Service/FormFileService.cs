using Microsoft.AspNetCore.Http;

namespace HikingTracks.Application;

public class FormFileService : IFormFileService
{
    public async Task<byte[]> IntoByteArray(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        // Convert the file if its longer than 2 MB
        if (stream.Length < 2097152)
        {
            return stream.ToArray();
        }
        else
        {
            throw new Exception($"Photo: {file.FileName} is bigger than 2 MB.");
        }
    }
}
