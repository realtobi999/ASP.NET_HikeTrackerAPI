using System.ComponentModel.DataAnnotations;

namespace HikingTracks.Domain;

public record class CreatePhotoDto
{
    public Guid HikeID { get; set; }

    public string? FileName { get; set; }

    public long Length { get; set; }

    public byte[] Content { get; set; } = [];
}
