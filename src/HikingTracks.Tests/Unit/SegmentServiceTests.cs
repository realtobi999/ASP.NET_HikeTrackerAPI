using FluentAssertions;
using HikingTracks.Application;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;
using Moq;

namespace HikingTracks.Tests;

public class SegmentServiceTests
{
    [Fact]
    public async void Segment_CreateSegment_Works()
    {
        // Arrange
        var createSegmentDto = new Segment().WithFakeData().ToCreateSegmentDto();

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Segment.CreateSegment(It.IsAny<Segment>()));

        var service = new SegmentService(repository.Object, logger.Object);

        // Act
        var segment = await service.CreateSegment(createSegmentDto);

        // Assert
        segment.Should().NotBeNull();
        segment.Name.Should().Be(createSegmentDto.Name);
        segment.Distance.Should().Be(createSegmentDto.Distance);
        segment.ElevationLoss.Should().Be(createSegmentDto.ElevationLoss);
        segment.Coordinates.Should().BeEquivalentTo(createSegmentDto.Coordinates);

        repository.Verify(repo => repo.Segment.CreateSegment(It.IsAny<Segment>()), Times.Once);
        repository.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async void Segment_GetAllSegments_Works()
    {
        // Arrange
        var segments = new List<Segment>
        {
            new Segment().WithFakeData(),
            new Segment().WithFakeData()
        };

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Segment.GetAllSegments()).ReturnsAsync(segments);

        var service = new SegmentService(repository.Object, logger.Object);

        // Act
        var retrievedSegments = await service.GetAllSegments();

        // Assert
        retrievedSegments.Should().NotBeNull();
        retrievedSegments.Should().BeEquivalentTo(segments);
    }

    [Fact]
    public async void Segment_GetSegment_Works()
    {
        // Arrange
        var segment = new Segment().WithFakeData(); 

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Segment.GetSegment(segment.ID)).ReturnsAsync(segment);

        var service = new SegmentService(repository.Object, logger.Object);

        // Act
        var retrievedSegment = await service.GetSegment(segment.ID);

        // Assert
        retrievedSegment.Should().NotBeNull();
        retrievedSegment.Should().BeEquivalentTo(segment);
    }

    [Fact]
    public async void Segment_GetSegment_FailsWhenNotFound()
    {
        // Arrange
        var segment = new Segment().WithFakeData();

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Segment.GetSegment(segment.ID)).ReturnsAsync(null as Segment);

        var service = new SegmentService(repository.Object, logger.Object);

        // Act & Assert
        await Assert.ThrowsAsync<SegmentNotFoundException>(async () => await service.GetSegment(segment.ID));
    }

    [Fact]
    public async void Segment_GetHikeSegments_Works()
    {
        // Prepare
        var hike = new Hike().WithFakeData();
        hike.Coordinates = [
            new(50,50,0),
            new(50.005, 50.002, 0),
            new(50.006, 50.003, 0),
            new(50.007, 50.003, 0),
            new(50.006, 50.003, 0)
        ];

        var segment1 = new Segment().WithFakeData();
        segment1.Coordinates = [
            new(50,50,0),
            new(50.004, 50.003, 0),
            new(50.005, 50.004, 0),
            new(50.006, 50.004, 0),
            new(50.005, 50.004, 0)
        ];

        var segment2 = new Segment().WithFakeData();
        segment2.Coordinates = [
            new(80,50,0),
            new(80.004, 50.003, 0),
            new(80.005, 50.004, 0),
            new(80.006, 50.004, 0),
            new(80.005, 50.004, 0)
        ];

        var segment3 = new Segment().WithFakeData();
        segment2.Coordinates = [
            new(50,50,0),
            new(80.004, 50.003, 0),
            new(80.005, 50.004, 0),
            new(80.006, 50.004, 0),
            new(80.005, 50.004, 0)
        ];

        var testSegments = new List<Segment>(){segment1, segment2, segment3};

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Segment.GetAllSegments()).ReturnsAsync(testSegments);

        var service = new SegmentService(repository.Object, logger.Object);

        // Act & Assert
        var segments = await service.GetHikeSegments(hike);

        segments.Should().NotBeEmpty();
        segments.Count().Should().Be(1);
        segments.ElementAt(0).Should().BeEquivalentTo(segment1);
    }
}
