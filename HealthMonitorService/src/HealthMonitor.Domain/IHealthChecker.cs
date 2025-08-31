namespace HealthMonitor.Domain;

public interface IHealthChecker
{
    /// <summary>
    /// This method checks the health of a server and returns a result.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HealthCheckResult> CheckAsync(Server server, CancellationToken cancellationToken = default);
}