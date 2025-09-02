using HealthMonitor.Domain;
using Microsoft.Extensions.Logging;

namespace HealthMonitor.Application;

// If we pass null to WriteAsync what happens
// How to protect it, and write test for it.
public sealed class HealthCheckService
{
    private readonly ILogger<HealthCheckService> _logger;
    private readonly IHealthChecker _healthChecker;
    private readonly IHealthCheckResultWriter _resultWriter;

    public HealthCheckService(
        ILogger<HealthCheckService> logger,
        IHealthChecker healthChecker,
        IHealthCheckResultWriter resultWriter)
    {
        _logger = logger;
        _healthChecker = healthChecker;
        _resultWriter = resultWriter;
    }

    public async Task CheckAndRecordAsync(Server server, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Started checking and recording health for {ServerName}", server.Name);
        
        var result = await _healthChecker.CheckAsync(server, cancellationToken);
        await _resultWriter.WriteAsync(result, cancellationToken);
        
        _logger.LogInformation("Completed checking and recording health for {ServerName}", server.Name);
    }
}