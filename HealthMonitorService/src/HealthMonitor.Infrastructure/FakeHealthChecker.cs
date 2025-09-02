using HealthMonitor.Domain;
using Microsoft.Extensions.Logging;

namespace HealthMonitor.Infrastructure;

/// <summary>
/// One possible implementation of IHealthChecker for demonstration purposes.
/// </summary>
public class FakeHealthChecker : IHealthChecker
{
    private readonly ILogger<FakeHealthChecker> _logger;

    public FakeHealthChecker(ILogger<FakeHealthChecker> logger)
    {
        _logger = logger;
    }
    
    public Task<HealthCheckResult> CheckAsync(Server server, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Checking health check of {ServerName}", server.Name);
    
        try
        {
            var isHealthy = server.IpAddress.EndsWith("1"); // fake rule

            var result = Task.FromResult(new HealthCheckResult
            {
                ServerName = server.Name,
                IsHealthy = isHealthy,
                Timestamp = DateTime.UtcNow,
                Message = isHealthy ? "Healthy" : "Unreachable"
            });
        
            _logger.LogInformation("Checked {ServerName} completed.", server.Name);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while checking health of {ServerName}", server.Name);

            return Task.FromResult(new HealthCheckResult
            {
                ServerName = server.Name,
                IsHealthy = false,
                Timestamp = DateTime.UtcNow,
                Message = $"Error: {e.Message}"
            });
        }
        
        
    }
}