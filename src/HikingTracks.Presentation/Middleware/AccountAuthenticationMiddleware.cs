using HikingTracks.Application;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Presentation;

public class AccountAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _token;

    public AccountAuthenticationMiddleware(RequestDelegate next, ITokenService token)
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

        var tokenAccountId = tokenPayload.FirstOrDefault(c => c.Type == "accountId")?.Value;
        if (tokenAccountId is null)
            throw new InvalidJwtTokenException("Token payload is missing accountId");

        // Get the url accountId from the url
        var queryAccountId = context.Request.RouteValues.FirstOrDefault(v => v.Key == "accountId").Value as string;

        // Verify if the JWT accountId matches the Id the user wants to modify 
        if (tokenAccountId != queryAccountId)
        {
            throw new NotAuthorizedException("Not Authorized!");
        }

        await _next(context);
    }
}

public static class AccountMiddlewareExtensions
{
    public static void UseAccountAuthenticationMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<AccountAuthenticationMiddleware>();
}
