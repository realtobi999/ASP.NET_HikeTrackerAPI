using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.DTO;

public record class CreateAccountDto
(
    Guid? ID,
    string Username,
    string Email,
    string Password 
);

