using FluentAssertions;
using HikingTracks.Application.Service;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;
using HikingTracks.Tests.Integration.AccountEndpointTests;
using Moq;

namespace HikingTracks.Tests.Unit;

public class HikeServiceTests
{
    [Fact]
    public async void Hike_GetAllHikes_Works()
    {
        // Prepare
        var hike1 = new Hike().WithFakeData();
        var hike2 = new Hike().WithFakeData();
        var hike3 = new Hike().WithFakeData();

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Hike.GetAllHikes()).ReturnsAsync(new List<Hike> { hike1, hike2, hike3 });

        var service = new HikeService(repository.Object, logger.Object);

        // Act & Assert
        var hikes = await service.GetAllHikes();

        hikes.Should().NotBeEmpty();
        hikes.Count().Should().Be(3);
        hikes.ElementAt(0).Should().BeEquivalentTo(hike1);
        hikes.ElementAt(1).Should().BeEquivalentTo(hike2);
        hikes.ElementAt(2).Should().BeEquivalentTo(hike3);
    }

    [Fact]
    public async void Hike_GetAllHikes_LimitAndOffsetWorks()
    {
        // Prepare
        var hike1 = new Hike().WithFakeData();
        var hike2 = new Hike().WithFakeData();
        var hike3 = new Hike().WithFakeData();

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Hike.GetAllHikes()).ReturnsAsync(new List<Hike> { hike1, hike2, hike3 });

        var service = new HikeService(repository.Object, logger.Object);

        // Act & Assert
        var limit = 2;
        var offset = 1;
        var hikes = await service.GetAllHikes(limit, offset);

        hikes.Should().NotBeEmpty();
        hikes.Count().Should().Be(limit);
        hikes.ElementAt(0).Should().BeEquivalentTo(hike2);
        hikes.ElementAt(1).Should().BeEquivalentTo(hike3);
    }

    [Fact]
    public async Task Hike_CreateHike_Works()
    {
        // Prepare
        var account = new Account().WithFakeData();
        var createHikeDto = new Hike().WithFakeData().ToCreateHikeDto();

        if (createHikeDto.ID is null) throw new Exception("Something went wrong.. ID cannot be null!");

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Account.GetAccount(account.ID)).ReturnsAsync(account);
        repository.Setup(repo => repo.Hike.CreateHike(It.IsAny<Hike>()));

        var service = new HikeService(repository.Object, logger.Object);

        // Act & Assert
        var hike = await service.CreateHike(account.ID, createHikeDto);

        hike.Should().NotBeNull();
        hike.ID.Should().Be((Guid)createHikeDto.ID);
        hike.AccountID.Should().Be(account.ID);
        hike.Title.Should().Be(createHikeDto.Title);
        hike.Description.Should().Be(createHikeDto.Description);
        hike.ElevationGain.Should().Be(createHikeDto.ElevationGain);
        hike.ElevationLoss.Should().Be(createHikeDto.ElevationLoss);
        hike.Coordinates.Should().BeEquivalentTo(createHikeDto.Coordinates);
    }

    [Fact]
    public async Task Hike_DeleteHike_Works()
    {
        // Prepare
        var hikeId = Guid.NewGuid();
        var hike = new Hike().WithFakeData();

        var repository = new Mock<IRepositoryManager>();
        var logger = new Mock<ILoggerManager>();

        repository.Setup(repo => repo.Hike.GetHike(hikeId)).ReturnsAsync(hike);

        var service = new HikeService(repository.Object, logger.Object);

        // Act & Assert
        await service.DeleteHike(hikeId);

        repository.Verify(repo => repo.Hike.DeleteHike(hike), Times.Once);
        repository.Verify(repo => repo.SaveAsync(), Times.Once);
    }

}
