using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HikingTracks.Presentation;

public static class JwtAuthenticationExtensions
{
    public static void ConfigureJwtAuthentication(this WebApplicationBuilder builder)
    {
        var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

        if (jwtIssuer is null)
        {
            throw new ArgumentNullException(jwtIssuer);
        }
        else if (jwtKey is null)
        {
            throw new ArgumentNullException(jwtKey);
        }

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });
    }
}
