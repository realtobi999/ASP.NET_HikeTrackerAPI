using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using HikingTracks.Application;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.Exceptions;

namespace HikingTracks.Presentation;

public class AccountAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AccountAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceManager _service)
    {
        // Skip the request if the corresponding controller doesnt have the correct attribute
        if (context.GetEndpoint()?.Metadata.GetMetadata<AccountAuthAttribute>() is null)
        {
            await _next(context);
            return;
        }

        var header = context.Request.Headers.Authorization.FirstOrDefault();
        if (header is null)
            throw new InvalidAuthHeaderException("Missing header: Bearer <JWT_TOKEN>");

        var token = _service.TokenService.ParseTokenFromAuthHeader(header);
        var tokenPayload = _service.TokenService.ParseTokenPayload(token);

        var tokenAccountId = tokenPayload.FirstOrDefault(c => c.Type == "accountId")?.Value;
        if (tokenAccountId is null)
            throw new InvalidJwtTokenException("Token payload is missing 'accountId'");

        // Get the url accountId from the url
        var contextAccountId = context.Request.RouteValues.FirstOrDefault(v => v.Key == "accountId").Value as string;

        if (contextAccountId is null)
        {
            // Read the request body into a string
            using StreamReader reader = new(context.Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            // Parse the request body as JSON
            var requestBodyJson = JsonObject.Parse(requestBody) ?? throw new AccountBadRequestException("Missing 'accountId' in request body.");
            contextAccountId = requestBodyJson["accountId"]?.ToString();

            if (contextAccountId is null)
                throw new AccountBadRequestException("Missing or invalid 'accountId' in request body.");

            // Create a new stream back to the request with the parsed JSON data
            var requestBodyBytes = Encoding.UTF8.GetBytes(requestBody);
            context.Request.Body = new MemoryStream(requestBodyBytes);
        }

        // Verify if the JWT accountId matches the Id the user wants to modify 
        if (tokenAccountId != contextAccountId)
        {
            throw new NotAuthorizedException("Not Authorized!");
        }

        await _next(context);
    }
}

public static class AccountMiddlewareExtensions
{
    public static void UseAccountAuthentication(this IApplicationBuilder builder) =>
        builder.UseMiddleware<AccountAuthenticationMiddleware>();
}
