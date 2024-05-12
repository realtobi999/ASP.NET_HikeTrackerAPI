using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Domain;

public class HikeBadRequestException(string message) : BadRequestException(message)
{

}
