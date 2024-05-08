using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HikingTracks.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HikingTracks.Application;

public class TokenService : ITokenService
{
    private readonly string _jwtIssuer;
    private readonly string _jwtKey;
    private IEnumerable<Claim> claims = [];

    public TokenService(string jwtIssuer, string jwtKey)
    {
        _jwtIssuer = jwtIssuer;
        _jwtKey = jwtKey;
    }

    public string CreateToken()
    {
        var key = Encoding.ASCII.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _jwtIssuer,
            Audience = _jwtIssuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public IEnumerable<Claim> ParseTokenPayload(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var payload = handler.ReadJwtToken(token).Claims;

        return payload;
    }

    public ITokenService WithPayload(IEnumerable<Claim> claims)
    {
        this.claims = claims;

        return this;
    }
}
