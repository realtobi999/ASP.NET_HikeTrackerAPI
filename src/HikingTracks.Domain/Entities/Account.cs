using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HikingTracks.Domain.DTO;

namespace HikingTracks.Domain.Entities;

public class Account
{
    [Column("id")]
    public Guid ID { get; set; }

    [Column("username")]
    [Required, MaxLength(255)]
    public string? Username { get; set; }

    [Column("email")]
    [Required, MaxLength(255)]
    public string? Email { get; set; }

    [Column("password")]
    [Required, MaxLength(255)]
    public string? Password { get; set; }

    [Column("token")]
    [Required]
    public Guid Token { get; set; }

    [Column("total_hikes")]
    [Required, DefaultValue(0)]
    [Range(0, int.MaxValue)]
    public int TotalHikes { get; set; }

    [Column("total_distance")]
    [Required, DefaultValue(0), MinLength(0)]
    [Range(0, double.MaxValue)]
    public double TotalDistance { get; set; }

    [Column("total_moving_time")]
    [Required, DefaultValue(0)]
    public TimeSpan TotalMovingTime { get; set; }

    [Column("created_at")]
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    public AccountDto ToDTO()
    {
        return new AccountDto(
            ID,
            Username!,
            Email!,
            TotalHikes,
            TotalDistance,
            TotalMovingTime,
            CreatedAt
        );
    }
}

