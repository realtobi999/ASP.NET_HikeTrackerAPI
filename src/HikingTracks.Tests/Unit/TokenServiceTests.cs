using System.Security.Claims;
using FluentAssertions;
using HikingTracks.Application;
using HikingTracks.Domain.Entities;
using HikingTracks.Tests.Integration.AccountEndpointTests;

namespace HikingTracks.Tests.Unit;

public class TokenServiceTests
{
    [Fact]
    public void TokenService_CreateToken_Works()
    {
        // Prepare
        var issuer = "test";
        var key = "y6G142JqXRqmcgO1Bsiy5R68Hll4gsZAD2G05GpjtK0ahJC7gc";
        var account = new Account().WithFakeData();
        var service = new TokenService(issuer, key);
       
        // Act & Assert
        var token = service.CreateToken();

        token.Should().NotBeNull();
        token.Length.Should().BeGreaterThan(55);
    }

    [Fact]
    public void TokenService_ParseTokenPayload_Works()
    {
        // Prepare
        var issuer = "test";
        var key = "y6G142JqXRqmcgO1Bsiy5R68Hll4gsZAD2G05GpjtK0ahJC7gc";
        var account = new Account().WithFakeData();
        var service = new TokenService(issuer, key);
        var claims = new List<Claim>(){
            new("AccountId", account.ID.ToString())  
        };
       
        // Act & Assert
        var token = service.WithPayload(claims).CreateToken();
        token.Should().NotBeNull();  

        var payload = service.ParseTokenPayload(token); 

        payload.Should().NotBeEmpty();
        payload.Count().Should().Be(4); // Include the issuers also, otherwise it would be 1.
        payload.FirstOrDefault(c => c.Type == "AccountId")?.Value.Should().Be(account.ID.ToString());
    }
}
