namespace HealthMonitor.Domain;

public interface IHealthChecker
{
    Task<HealthCheckResult> CheckAsync(Server server, CancellationToken cancellationToken = default);
}