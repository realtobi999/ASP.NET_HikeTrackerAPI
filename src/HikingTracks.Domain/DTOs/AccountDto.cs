namespace HikingTracks.Domain.DTO;

public record class AccountDto
(
    Guid ID,
    string Username,
    string Email,
    int TotalHikes,
    double TotalDistance,
    TimeSpan TotalMovingTime,
    DateTimeOffset CreatedAt
);


