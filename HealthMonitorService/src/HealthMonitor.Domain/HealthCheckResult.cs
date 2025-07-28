namespace HealthMonitor.Domain;

public sealed class HealthCheckResult
{
    // I want to enfore using the Create factory method by making the constructor private.
    private HealthCheckResult()
    {
    }
    
    public int Id { get; init; }
    public required string ServerName { get; init; }
    public DateTime Timestamp { get; init; }
    public bool IsHealthy { get; private set; }
    public string? Message { get; private set; }

    public static HealthCheckResult Create(string serverName)
        => new()
        {
            ServerName = serverName,
            Timestamp = DateTime.UtcNow
        };

    public void MarkAsHealthy(string message) => SetHealthStatus(true, message);

    public void MarkAsUnhealthy(string message) => SetHealthStatus(false, message);
    
    private void SetHealthStatus(bool isHealthy, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty.", nameof(message));

        IsHealthy = isHealthy;
        Message = message;
    }
}
