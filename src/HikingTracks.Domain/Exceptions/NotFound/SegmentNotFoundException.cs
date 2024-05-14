namespace HikingTracks.Domain.Exceptions;

public class SegmentNotFoundException(Guid Id) : NotFoundException($"The account with the id: {Id} doesn't exist.")
{

}
