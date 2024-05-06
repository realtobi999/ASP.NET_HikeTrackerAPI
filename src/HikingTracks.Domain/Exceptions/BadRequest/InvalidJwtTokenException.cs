namespace HikingTracks.Domain.Exceptions;

public class InvalidJwtTokenException(string message) : BadRequestException(message)
{
}
