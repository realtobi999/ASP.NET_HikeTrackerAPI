using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Domain;

public class AccountBadCredentialsException(string email, string password) : BadRequestException($"The credentials {email} & {password} could not be verified.")
{
}
