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
}
