namespace HealthMonitor.Domain;

public sealed class HealthCheckResult
{
    public required string ServerName { get; init; }
    public required DateTime Timestamp { get; init; }
    public bool IsHealthy { get; init; }
    public string? Message { get; init; }
}