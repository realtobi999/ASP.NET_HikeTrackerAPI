namespace HikingTracks.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(string accountID);
}
