using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Domain;

public class HikeNotFoundException(Guid Id) : NotFoundException($"The account with the id: {Id} doesn't exist.")
{

}
