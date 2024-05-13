using HikingTracks.Application;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Presentation;

public class HikeAuthenticationMIddleware
{
    private readonly RequestDelegate _next;

    public HikeAuthenticationMIddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceManager _service)
    {
        // Skip the request if the corresponding controller doesnt have the correct attribute
        if(context.GetEndpoint()?.Metadata.GetMetadata<HikeAuthAttribute>() is null)
        {
            await _next(context);
            return;
        }

        // Get the accountId from the JWT token payload
        var header = context.Request.Headers.Authorization.FirstOrDefault();
        if (header is null)
            throw new InvalidAuthHeaderException("Missing header: Bearer <JWT_TOKEN>");

        var token = header.Split(" ").Last().Trim();
        if (token is null)
            throw new InvalidAuthHeaderException("Bad authentication header format. Try: Bearer <JWT_TOKEN>");

        var tokenPayload = _service.TokenService.ParseTokenPayload(token);

        var accountId = tokenPayload.FirstOrDefault(c => c.Type == "accountId")?.Value;
        if (accountId is null)
            throw new InvalidJwtTokenException("Token payload is missing accountId");

        // Get the hikeId from the url
        var hikeId = context.Request.RouteValues.FirstOrDefault(v => v.Key == "hikeId").Value as string;
        if (hikeId is null)
            throw new HikeBadRequestException("The request is missing hikeId");

        // Check if the hike accountId matches the sender accountId
        var hike = await _service.HikeService.GetHike(Guid.Parse(hikeId));

        if (hike.AccountId.ToString() != accountId)
        {
            throw new NotAuthorizedException("Not Authorized!");
        }

        await _next(context);
    }
}

public static class HikeMiddlewareExtensions
{
    public static void UseHikeAuthentication(this IApplicationBuilder builder) =>
        builder.UseMiddleware<HikeAuthenticationMIddleware>();
}
