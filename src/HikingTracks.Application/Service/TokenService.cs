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

    public TokenService(string jwtIssuer, string jwtKey)
    {
        _jwtIssuer = jwtIssuer;
        _jwtKey = jwtKey;
    }

    public string CreateToken(string accountID)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("AccountID", accountID)
        };

        var secToken = new JwtSecurityToken(
            _jwtIssuer,
            _jwtIssuer,
            claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(secToken);
    }
}
