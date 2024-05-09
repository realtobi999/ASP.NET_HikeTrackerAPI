using Microsoft.AspNetCore.Http;

namespace HikingTracks.Application;

public class FormFileService : IFormFileService
{
    public async Task<byte[]> IntoByteArray(IFormFile file)
    {
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);

            // Check if the file size is within limits
            if (stream.Length < 2097152) // 2 MB in bytes
            {
                return stream.ToArray();
            }
            else
            {
                throw new Exception($"File '{file.FileName}' is bigger than 2 MB.");
            }
        }
    }
}

