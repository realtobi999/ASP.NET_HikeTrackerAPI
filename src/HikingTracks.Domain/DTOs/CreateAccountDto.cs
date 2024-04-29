using HikingTracks.Domain.Entities;

namespace HikingTracks.Domain.DTO;

public record class CreateAccountDto
(
    string Username,
    string Email,
    string Password 
);

