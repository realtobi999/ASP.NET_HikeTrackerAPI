using Xunit;
using Moq;
using FluentAssertions;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using HikingTracks.Application;

public class FormFileServiceTests
{
    [Fact]
    public async Task IntoByteArray_FileWithinSizeLimit_ReturnsByteArray()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        var stream = new MemoryStream();
        var fileContent = new byte[] { 0x12, 0x34, 0x56, 0x78 }; // Example file content
        fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default))
            .Callback<Stream, CancellationToken>((outputStream, _) =>
            {
                outputStream.Write(fileContent, 0, fileContent.Length);
            })
            .Returns(Task.CompletedTask);

        var formFileService = new FormFileService();

        // Act & Assert
        var byteArray = await formFileService.IntoByteArray(fileMock.Object);

        byteArray.Should().NotBeNull();
        byteArray.Should().BeEquivalentTo(fileContent);
    }

    [Fact]
    public async Task IntoByteArray_FileExceedsSizeLimit_ThrowsException()
    {
        // Prepare
        var fileMock = new Mock<IFormFile>();
        var stream = new MemoryStream();
        var fileContent = new byte[2097153]; // File size exceeds limit
        fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default))
            .Callback<Stream, CancellationToken>((outputStream, _) =>
            {
                outputStream.Write(fileContent, 0, fileContent.Length);
            })
            .Returns(Task.CompletedTask);

        var formFileService = new FormFileService();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => formFileService.IntoByteArray(fileMock.Object));
    }
}