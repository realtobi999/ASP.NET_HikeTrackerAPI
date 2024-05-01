namespace HikingTracks.Domain;

public class InvalidCoordinateException(string message) : BadRequestException(message)
{

}
