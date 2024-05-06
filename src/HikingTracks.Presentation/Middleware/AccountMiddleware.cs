using HikingTracks.Application;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Presentation;

public class AccountMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _token;

    public AccountMiddleware(RequestDelegate next, ITokenService token)
    {
        _next = next;
        _token = token;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip the request if the corresponding controller doesnt have the AccountAuth attribute
        if(context.GetEndpoint()?.Metadata.GetMetadata<AccountAuthAttribute>() is null)
        {
            await _next(context);
            return;
        }

        var header = context.Request.Headers.Authorization.FirstOrDefault();
        if (header is null)
            throw new InvalidAuthHeaderException("Missing Header: Bearer <JWT_TOKEN>");

        var token = header.Split(" ").Last().Trim();
        if (token is null)
            throw new InvalidAuthHeaderException("Bad Authentication Header Format. Try: Bearer <JWT_TOKEN>");

        var tokenPayload = _token.ParseTokenPayload(token);

        var tokenAccountId = tokenPayload.FirstOrDefault(c => c.Type == "AccountId")?.Value;
        if (tokenAccountId is null)
            throw new InvalidJwtTokenException("Token payload is missing AccountId");

        var queryAccountId = context.Request.Query["accountId"];

        // Verify if the JWT AccountId matches the Id the user wants to modify 
        if (tokenAccountId != queryAccountId)
        {
            throw new NotAuthorizedException("Not Authorized!");
        }

        await _next(context);
    }
}

public static class AccountMiddlewareExtensions
{
    public static void UseAccountMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<AccountMiddleware>();
}
