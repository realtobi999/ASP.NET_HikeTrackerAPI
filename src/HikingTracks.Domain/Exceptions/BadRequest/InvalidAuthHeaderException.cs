using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Domain.Exceptions;

public class InvalidAuthHeaderException(string message) : BadRequestException(message)
{

}
