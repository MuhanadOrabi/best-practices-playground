namespace HealthMonitor.Domain;

public interface IHealthCheckResultWriter
{
    Task WriteAsync(HealthCheckResult result, CancellationToken cancellationToken = default);
}