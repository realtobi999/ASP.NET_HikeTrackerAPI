using System.Net.Http.Json;
using FluentAssertions;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Presentation;
using Xunit.Sdk;

namespace HikingTracks.Tests.Integration.AccountTests;

public class AccountControllerTests
{
    [Fact]
    public async Task Account_GetAccounts_ReturnsEmptyWhenNoAccounts()
    {
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var response = await client.GetAsync("/api/account");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadFromJsonAsync<List<AccountDto>>()).Should().BeEmpty();
    }

    [Fact]
    public async Task Account_GetAccount_Works()
    {
        var client = new WebAppFactory<Program>().CreateDefaultClient(); 
        var account = new Account().WithFakeData();

        var create = await  client.PostAsJsonAsync("/api/account", account);
        create.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var response = await client.GetAsync(string.Format("/api/account/{0}", account.ID));

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var responseBody = await response.Content.ReadFromJsonAsync<AccountDto>() ?? throw new Exception("Failed to deserialize the response body into an AccountDto object.");
        responseBody.ID.Should().Be(account.ID);
    }

    [Fact]
    public async Task Account_CreateAccount_Works()
    {
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var account = new Account().WithFakeData();

        var response = await  client.PostAsJsonAsync("/api/account", account);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        response.Headers.Contains("Location").Should().BeTrue();

        var header = response.Headers.GetValues("Location");

        header.Should().Equal(string.Format("/api/account/{0}", account.ID));
    }
}
