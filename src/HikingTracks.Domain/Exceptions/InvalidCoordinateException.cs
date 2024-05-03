namespace HikingTracks.Domain.Exceptions;

public class InvalidCoordinateException(string message) : BadRequestException(message)
{
}
