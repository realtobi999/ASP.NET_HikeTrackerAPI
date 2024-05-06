namespace HikingTracks.Domain.Exceptions;

public class AccountNotFoundException(Guid Id) : NotFoundException($"The account with the id: {Id} doesn't exist.")
{
}
