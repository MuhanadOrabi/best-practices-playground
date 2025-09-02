using HealthMonitor.Domain;
using Microsoft.Extensions.Logging;

namespace HealthMonitor.Infrastructure;

public class ConsoleResultWriter : IHealthCheckResultWriter
{
    private readonly ILogger<ConsoleResultWriter> _logger;

    public ConsoleResultWriter(ILogger<ConsoleResultWriter> logger)
    {
        _logger = logger;
    }
    
    public Task WriteAsync(HealthCheckResult result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Writing health check result to console for {ServerName}", result.ServerName);

        try
        {
            Console.WriteLine($"[{result.Timestamp}] {result.ServerName} - Status: {(result.IsHealthy ? "OK" : "FAIL")} - {result.Message}");
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while writing health check result for {ServerName}: {ErrorMessage}", result.ServerName, e.Message);
        }
        
        return Task.CompletedTask;
    }
}