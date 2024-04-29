namespace HikingTracks.Domain.Exceptions;

public class AccountNotFoundException(Guid id) : NotFoundException($"The account with the id: {id} doesn't exist.")
{
}
