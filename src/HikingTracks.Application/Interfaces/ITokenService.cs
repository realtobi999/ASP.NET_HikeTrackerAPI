using System.Security.Claims;

namespace HikingTracks.Application.Interfaces;

public interface ITokenService
{
    string CreateToken();
    string ParseTokenFromAuthHeader(string header);
    IEnumerable<Claim> ParseTokenPayload(string token);
    ITokenService WithPayload(IEnumerable<Claim> claims);
}
