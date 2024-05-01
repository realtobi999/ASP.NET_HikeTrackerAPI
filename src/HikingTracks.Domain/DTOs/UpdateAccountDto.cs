namespace HikingTracks.Domain;

public record class UpdateAccountDto
(
    string Username,
    string Email,
    int TotalHikes,
    double TotalDistance,
    TimeSpan TotalMovingTime
);
