using FluentAssertions;

namespace HealthMonitor.Domain.Tests.Unit;

public class HealthCheckResultTests
{
    [Fact]
    public void HealthCheckResult_Create_SetsPropertiesCorrectly()
    {
        // Arrange
        var result = HealthCheckResult.Create("Server A");

        // Act
        // Assert
        result.ServerName.Should().Be("Server A");
        result.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
    
    [Fact]
    public void HealthCheckResult_MarkAsHealthy_SetsPropertiesCorrectly()
    {
        // Arrange
        var result = HealthCheckResult.Create("Server A");

        // Act
        result.MarkAsHealthy("All good");

        // Assert
        result.IsHealthy.Should().BeTrue();
        result.Message.Should().Be("All good");
    }

    [Fact]
    public void HealthCheckResult_MarkAsUnhealthy_SetsPropertiesCorrectly()
    {
        // Arrange
        var result = HealthCheckResult.Create("Server B");

        // Act
        result.MarkAsUnhealthy("Timeout");

        // Assert
        result.IsHealthy.Should().BeFalse();
        result.Message.Should().Be("Timeout");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void HealthCheckResult_SetHealthStatus_ThrowsOnEmptyMessage(string? message)
    {
        // Arrange
        var result = HealthCheckResult.Create("Invalid Server");

        // Act
        var act = () => result.MarkAsHealthy(message!);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Message cannot be empty. (Parameter 'message')");
    }
}