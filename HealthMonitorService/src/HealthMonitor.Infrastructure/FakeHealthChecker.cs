using HealthMonitor.Domain;

namespace HealthMonitor.Infrastructure;

public class FakeHealthChecker : IHealthChecker
{
    public Task<HealthCheckResult> CheckAsync(Server server, CancellationToken cancellationToken = default)
    {
        var isHealthy = server.IpAddress.EndsWith("1"); // fake rule

        return Task.FromResult(new HealthCheckResult
        {
            ServerName = server.Name,
            IsHealthy = isHealthy,
            Timestamp = DateTime.UtcNow,
            Message = isHealthy ? "Healthy" : "Unreachable"
        });
    }
}