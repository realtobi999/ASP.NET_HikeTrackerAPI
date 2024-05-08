using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain;

public class Photo
{
    [Required, Column("id")]
    public Guid ID { get; set; }

    [Required, Column("hike_id")]
    public Guid HikeID { get; set; }

    [Required, Column("file_name")]
    public string? FileName { get; set; }

    [Required, Column("length")]
    public long Length { get; set; }

    [Required, Column("content")]
    public byte[] Content { get; set; } = [];

    [JsonIgnore]
    public Hike? Hike { get; set; }
}
