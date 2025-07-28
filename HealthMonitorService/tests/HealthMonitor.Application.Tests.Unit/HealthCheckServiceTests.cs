using HealthMonitor.Domain;
using Moq;

namespace HealthMonitor.Application.Tests.Unit;

// Tests should be in AAA format
// Need to teach about: MOQ (what is it - youtube video or simple documentatino), FluentAssertions
public sealed class HealthCheckServiceTests
{
    [Fact]
    public async Task CheckAndRecordAsync_CallsCheckerAndWriter()
    {
        // Arrange
        var server = new Server { Name = "srv01", IpAddress = "10.0.0.1", Type = "iDRAC", Username = "admin", Password = "pass" };
        var expectedResult = HealthCheckResult.Create(server.Name);
        expectedResult.MarkAsHealthy("Healthy");

        var checkerMock = new Mock<IHealthChecker>();
        var writerMock = new Mock<IHealthCheckResultWriter>();

        checkerMock
            .Setup(c => c.CheckAsync(server, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var sut = new HealthCheckService(checkerMock.Object, writerMock.Object);

        // Act
        await sut.CheckAndRecordAsync(server);

        // Assert
        checkerMock.Verify(c => c.CheckAsync(server, It.IsAny<CancellationToken>()), Times.Once);
        writerMock.Verify(w => w.WriteAsync(expectedResult, It.IsAny<CancellationToken>()), Times.Once);
    }
}