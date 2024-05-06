using System.Security.Claims;

namespace HikingTracks.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(string accountID);
    IEnumerable<Claim> ParseTokenPayload(string token);
}
