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
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var hike = new Hike().WithFakeData();
        var account = new Account().WithFakeData();

        var create = await client.PostAsJsonAsync("/api/account", account);
        create.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert

        var response = await client.PostAsJsonAsync(string.Format("/api/account/{0}/hike", account.ID), hike.ToCreateHikeDto());

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        response.Headers.Contains("Location").Should().BeTrue();

        var header = response.Headers.GetValues("Location");
        header.Should().Equal(string.Format("/api/hike/{0}", hike.ID));
    }

    [Fact]
    public async Task Hike_GetHike_ReturnsOk()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var hike = new Hike().WithFakeData();
        var account = new Account().WithFakeData();

        var create1 = await client.PostAsJsonAsync("/api/account", account);
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var create2 = await client.PostAsJsonAsync(string.Format("/api/account/{0}/hike", account.ID), hike.ToCreateHikeDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert
        
        var response = await client.GetAsync(string.Format("/api/hike/{0}", hike.ID));

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]   
    public async Task Hike_GetHikes_ReturnsOk()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var account = new Account().WithFakeData();
        var hike1 = new Hike().WithFakeData();
        var hike2 = new Hike().WithFakeData();

        var create1 = await client.PostAsJsonAsync("/api/account", account);
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var create2 = await client.PostAsJsonAsync(string.Format("/api/account/{0}/hike", account.ID), hike1.ToCreateHikeDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

       var create3 = await client.PostAsJsonAsync(string.Format("/api/account/{0}/hike", account.ID), hike2.ToCreateHikeDto());
       create3.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert

        var response = await client.GetAsync("/api/hike");     
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        
        var body = await response.Content.ReadFromJsonAsync<List<HikeDto>>() ?? throw new Exception("Failed to deserialize the response body into an AccountDto object.");

        body.Should().NotBeEmpty();
        body.Count.Should().Be(2);
        body.ElementAt(0).ID.Should().Be(hike1.ID);
        body.ElementAt(1).ID.Should().Be(hike2.ID); 
    }

    [Fact]
    public async Task Hike_DeleteHike_ReturnsOKAsync()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var hike = new Hike().WithFakeData();
        var account = new Account().WithFakeData();
 
        var create1 = await client.PostAsJsonAsync("/api/account", account);
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var create2 = await client.PostAsJsonAsync(string.Format("/api/account/{0}/hike", account.ID), hike.ToCreateHikeDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert
    
        var response = await client.DeleteAsync(string.Format("/api/hike/{0}", hike.ID));
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        (await client.GetAsync(string.Format("/api/hike/{0}", hike.ID))).StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
