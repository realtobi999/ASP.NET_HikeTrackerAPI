using System.Net.Http.Json;
using FluentAssertions;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Presentation;
using HikingTracks.Tests.Integration.AccountEndpointTests;

namespace HikingTracks.Tests.Integration.HikeEndpointTests;

public class HikeControllerTests
{
    [Fact]
    public async Task Hike_CreateHike_ReturnsCreated()
    {
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var hike = new Hike().WithFakeData();
        var account = new Account().WithFakeData();

        var create = await client.PostAsJsonAsync("/api/account", account);
        create.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert

        var createAccountDto = new CreateHikeDto{
            Id = hike.ID,
            Distance = hike.Distance,
            ElevationGain = hike.ElevationGain,
            ElevationLoss = hike.ElevationLoss,
            MaxSpeed = hike.MaxSpeed,
            MovingTime = hike.MovingTime,
            Coordinates = hike.Coordinates
        };

        var response = await client.PostAsJsonAsync(string.Format("/api/hike/{0}", account.ID), createAccountDto);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        response.Headers.Contains("Location").Should().BeTrue();

        var header = response.Headers.GetValues("Location");
        header.Should().Equal(string.Format("/api/hike/{0}", hike.ID));
    }

    [Fact]
    public async Task Hike_GetHike_ReturnsOk()
    {
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var hike = new Hike().WithFakeData();
        var account = new Account().WithFakeData();

         var create1 = await client.PostAsJsonAsync("/api/account", account);
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var createAccountDto = new CreateHikeDto{
            Id = hike.ID,
            Distance = hike.Distance,
            ElevationGain = hike.ElevationGain,
            ElevationLoss = hike.ElevationLoss,
            MaxSpeed = hike.MaxSpeed,
            MovingTime = hike.MovingTime,
            Coordinates = hike.Coordinates
        };

        var create2 = await client.PostAsJsonAsync(string.Format("/api/hike/{0}", account.ID), createAccountDto);
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert
        
        var response = await client.GetAsync(string.Format("/api/hike/{0}", hike.ID));

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
