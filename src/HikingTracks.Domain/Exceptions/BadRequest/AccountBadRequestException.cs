using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Domain;

public class AccountBadRequestException(string message) : BadRequestException(message)
{

}
