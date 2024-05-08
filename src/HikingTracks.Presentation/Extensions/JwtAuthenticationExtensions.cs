using System.Text;
using HikingTracks.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HikingTracks.Presentation;

public static class JWTAuthenticationExtensions
{
    public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();

        if (jwtIssuer is null)
        {
            throw new ArgumentNullException(nameof(jwtIssuer), "JWT Issuer configuration is missing");
        }
        else if (jwtKey is null)
        {
            throw new ArgumentNullException(nameof(jwtKey), "JWT Key configuration is missing");
        }

        services.ConfigureTokenService(jwtIssuer, jwtKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });

    }
}

