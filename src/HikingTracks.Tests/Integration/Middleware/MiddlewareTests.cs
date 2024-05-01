using System.Net.Http.Json;
using FluentAssertions;
using HikingTracks.Domain.Entities;
using HikingTracks.Presentation;

namespace HikingTracks.Tests.Integration.Middleware;

public class MiddlewareTests
{
    [Fact]
    public async Task App_TestThatGlobalErrorHandlingWorksAsync()
    {
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var response = await client.GetAsync(string.Format("/api/account/{0}", Guid.Empty));

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        var body = await response.Content.ReadFromJsonAsync<ErrorDetails>() ?? throw new Exception("Failed to deserialize the response body into an AccountDto object.");

        body.StatusCode.Should().Be(404);
        body.Message.Should().Be("The account with the id: 00000000-0000-0000-0000-000000000000 doesn't exist.");
    }
}
