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
        var account = new Account().WithFakeData();
        var hike = new Hike().WithFakeData(account);

        var create = await client.PostAsJsonAsync("/api/account", account.ToCreateAccountDto());
        create.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Authenticate the request
        var login = await client.PostAsJsonAsync("/api/login", account.ToLoginAccountDto());
        login.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var token = await login.Content.ReadFromJsonAsync<TokenDto>();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        // Act & Assert
        var response = await client.PostAsJsonAsync("/api/hike", hike.ToCreateHikeDto());

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
        var account = new Account().WithFakeData();
        var hike = new Hike().WithFakeData(account);

        var create1 = await client.PostAsJsonAsync("/api/account", account.ToCreateAccountDto());
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Authenticate the request
        var login = await client.PostAsJsonAsync("/api/login", account.ToLoginAccountDto());
        login.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var token = await login.Content.ReadFromJsonAsync<TokenDto>();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        var create2 = await client.PostAsJsonAsync("/api/hike", hike.ToCreateHikeDto());
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
        var hike1 = new Hike().WithFakeData(account);
        var hike2 = new Hike().WithFakeData(account);

        var create1 = await client.PostAsJsonAsync("/api/account", account.ToCreateAccountDto());
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Authenticate the request
        var login = await client.PostAsJsonAsync("/api/login", account.ToLoginAccountDto());
        login.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var token = await login.Content.ReadFromJsonAsync<TokenDto>();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        var create2 = await client.PostAsJsonAsync("/api/hike", hike1.ToCreateHikeDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var create3 = await client.PostAsJsonAsync("/api/hike", hike2.ToCreateHikeDto());
        create3.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert

        var response = await client.GetAsync("/api/hike");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<List<HikeDto>>() ?? throw new Exception("Failed to deserialize the response body into HikeDto object.");

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
        var account = new Account().WithFakeData();
        var hike = new Hike().WithFakeData(account);

        var create1 = await client.PostAsJsonAsync("/api/account", account.ToCreateAccountDto());
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Authenticate the request
        var login = await client.PostAsJsonAsync("/api/login", account.ToLoginAccountDto());
        login.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var token = await login.Content.ReadFromJsonAsync<TokenDto>();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        var create2 = await client.PostAsJsonAsync("/api/hike", hike.ToCreateHikeDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert

        var response = await client.DeleteAsync(string.Format("/api/hike/{0}", hike.ID));
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        (await client.GetAsync(string.Format("/api/hike/{0}", hike.ID))).StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Hike_GetHikes_LimitAndOffsetWorks()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var account = new Account().WithFakeData();
        var hike1 = new Hike().WithFakeData(account);
        var hike2 = new Hike().WithFakeData(account);
        var hike3 = new Hike().WithFakeData(account);

        var create1 = await client.PostAsJsonAsync("/api/account", account.ToCreateAccountDto());
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Authenticate the request
        var login = await client.PostAsJsonAsync("/api/login", account.ToLoginAccountDto());
        login.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var token = await login.Content.ReadFromJsonAsync<TokenDto>();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        var create2 = await client.PostAsJsonAsync("/api/hike", hike1.ToCreateHikeDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var create3 = await client.PostAsJsonAsync("/api/hike", hike2.ToCreateHikeDto());
        create3.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var create4 = await client.PostAsJsonAsync("/api/hike", hike3.ToCreateHikeDto());
        create4.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert
        var limit = 2;
        var offset = 1;
        var response = await client.GetAsync(string.Format("/api/hike?limit={0}&offset={1}", limit, offset));
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<List<HikeDto>>() ?? throw new Exception("Failed to deserialize the response body into an HikeDto object.");

        body.Should().NotBeEmpty();
        body.Count.Should().Be(limit);
        body.ElementAt(0).ID.Should().Be(hike2.ID);
        body.ElementAt(1).ID.Should().Be(hike3.ID);
    }

    [Fact]
    public async Task Hike_GetHikes_FilteringByAccountWorks()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var account1 = new Account().WithFakeData();
        var account2 = new Account().WithFakeData();

        var create1 = await client.PostAsJsonAsync("/api/account", account1.ToCreateAccountDto());
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Authenticate the request
        var login1 = await client.PostAsJsonAsync("/api/login", account1.ToLoginAccountDto());
        login1.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var token1 = await login1.Content.ReadFromJsonAsync<TokenDto>();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token1!.Token);

        for (int i = 0; i < 2; i++)
        {
            var create3 = await client.PostAsJsonAsync("/api/hike", new Hike().WithFakeData(account1).ToCreateHikeDto());
            create3.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        client.DefaultRequestHeaders.Remove("Authorization"); 

        var create2 = await client.PostAsJsonAsync("/api/account", account2.ToCreateAccountDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Authenticate the request
        var login2 = await client.PostAsJsonAsync("/api/login", account2.ToLoginAccountDto());
        login2.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var token2 = await login2.Content.ReadFromJsonAsync<TokenDto>();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token2!.Token);

        for (int i = 0; i < 2; i++)
        {
            var create4 = await client.PostAsJsonAsync("/api/hike", new Hike().WithFakeData(account2).ToCreateHikeDto());
            create4.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        // Act & Assert
        var response = await client.GetAsync(string.Format("/api/hike?accountId={0}", account2.ID));
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<List<HikeDto>>() ?? throw new Exception("Failed to deserialize the response body into an HikeDto object.");

        body.Should().NotBeEmpty();
        body.ElementAt(0).AccountId.Should().Be(account2.ID);
        body.ElementAt(1).AccountId.Should().Be(account2.ID);
    }
}
