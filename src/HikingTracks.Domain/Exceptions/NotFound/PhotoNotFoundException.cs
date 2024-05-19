namespace HikingTracks.Domain.Exceptions;

public class PhotoNotFoundException(Guid Id) : NotFoundException($"The photo with the id: {Id} doesnt exist.")
{
}
