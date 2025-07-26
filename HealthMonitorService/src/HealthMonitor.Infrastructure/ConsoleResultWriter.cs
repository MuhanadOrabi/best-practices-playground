using HealthMonitor.Domain;

namespace HealthMonitor.Infrastructure;

public class ConsoleResultWriter : IHealthCheckResultWriter
{
    public Task WriteAsync(HealthCheckResult result, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[{result.Timestamp}] {result.ServerName} - Status: {(result.IsHealthy ? "OK" : "FAIL")} - {result.Message}");
        return Task.CompletedTask;
    }
}