using System.Net.Http.Json;
using FluentAssertions;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Presentation;

namespace HikingTracks.Tests;

public class SegmentControllerTests
{
    [Fact]
    public async Task Segment_CreateSegment_ReturnsCreated()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var segment = new Segment().WithFakeData();

        // Act & Assert
        var response = await client.PostAsJsonAsync("/api/segment", segment.ToCreateSegmentDto());
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var header = response.Headers.GetValues("Location");
        header.Should().Equal(string.Format("/api/segment/{0}", segment.ID));
    }

    [Fact]
    public async Task Segment_GetSegments_ReturnsOk()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var segment1 = new Segment().WithFakeData();
        var segment2 = new Segment().WithFakeData();
        var segment3 = new Segment().WithFakeData();

        var create1 = await client.PostAsJsonAsync("/api/segment", segment1.ToCreateSegmentDto());
        create1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        var create2 = await client.PostAsJsonAsync("/api/segment", segment2.ToCreateSegmentDto());
        create2.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        var create3 = await client.PostAsJsonAsync("/api/segment", segment3.ToCreateSegmentDto());
        create3.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert
        var limit = 2;
        var offset = 1;
        var response = await client.GetAsync(string.Format("/api/segment?limit={0}&offset={1}", limit, offset));
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<List<SegmentDto>>() ?? throw new Exception("Failed to deserialize the response body into SegmentDto object.");

        body.Count.Should().Be(limit);
        body.ElementAt(0).ID.Should().Be(segment2.ID);
        body.ElementAt(0).Coordinates.Should().BeEquivalentTo(segment2.Coordinates);
        body.ElementAt(1).ID.Should().Be(segment3.ID);
        body.ElementAt(1).Coordinates.Should().BeEquivalentTo(segment3.Coordinates);
    }

    [Fact]
    public async Task Segment_GetSegment_ReturnsOk()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var segment = new Segment().WithFakeData();

        var create = await client.PostAsJsonAsync("/api/segment", segment.ToCreateSegmentDto());
        create.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert
        var response = await client.GetAsync(string.Format("/api/segment/{0}", segment.ID));
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<SegmentDto>() ?? throw new Exception("Failed to deserialize the response body into SegmentDto object.");

        body.ID.Should().Be(segment.ID);
    }

    [Fact]
    public async Task Segment_UpdateSegment_ReturnsOk()
    {
        // Prepare
        var client = new WebAppFactory<Program>().CreateDefaultClient();
        var segment = new Segment().WithFakeData();

        var create = await client.PostAsJsonAsync("/api/segment", segment.ToCreateSegmentDto());
        create.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        // Act & Assert
        segment.Name = "TEST";
        segment.Distance = 123;

        var response = await client.PutAsJsonAsync(string.Format("/api/segment/{0}", segment.ID), segment.ToUpdateSegmentDto());
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK); 

        var get = await client.GetAsync(string.Format("/api/segment/{0}", segment.ID));
        get.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await get.Content.ReadFromJsonAsync<SegmentDto>() ?? throw new Exception("Failed to deserialize the response body into SegmentDto object.");

        body.ID.Should().Be(segment.ID);
        body.Name.Should().Be("TEST");
        body.Distance.Should().Be(123);
    }
}
