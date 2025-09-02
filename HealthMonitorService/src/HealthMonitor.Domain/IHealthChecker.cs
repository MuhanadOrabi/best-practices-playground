namespace HealthMonitor.Domain;

public interface IHealthChecker
{
    /// <summary>
    /// This method checks the health of the specified server and returns a HealthCheckResult.
    /// This never returns null - in case of an error - it returns a HealthCheckResult with IsHealthy set to false and an appropriate message.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>It returns HealthCheckResult with all the different success/fail scenarios.</returns>
    Task<HealthCheckResult> CheckAsync(Server server, CancellationToken cancellationToken = default);
}