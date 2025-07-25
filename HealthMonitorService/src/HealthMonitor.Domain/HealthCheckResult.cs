namespace HealthMonitor.Domain;

public class HealthCheckResult
{
    public string ServerName { get; init; }
    public DateTime Timestamp { get; init; }
    public bool IsHealthy { get; init; }
    public string? Message { get; init; }
}