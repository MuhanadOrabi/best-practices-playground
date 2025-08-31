using FluentAssertions;
using HealthMonitor.Domain;
using HealthMonitor.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HealthMonitor.Infrastructure.Tests.Integration;

public class HealthCheckResultWriterTests : TestBase
{
    [Fact]
    public async Task WriteAsync_ShouldPersistHealthCheckResult()
    {
        // Arrange
        const string serverName = "TestServer";

        var healthCheckResult = HealthCheckResult.Create(serverName);
        healthCheckResult.MarkAsHealthy("Success");
        
        using var scope = TestHost.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<HealthMonitorDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbHealthCheckResultWriter>>();
        var writer = new DbHealthCheckResultWriter(db, logger);

        // Act
        await writer.WriteAsync(healthCheckResult);

        // Assert
        var savedResult = await db.HealthCheckResults
            .FirstOrDefaultAsync(r => r.ServerName == serverName);

        savedResult.Should().NotBeNull();
        savedResult!.IsHealthy.Should().BeTrue();
    }
}