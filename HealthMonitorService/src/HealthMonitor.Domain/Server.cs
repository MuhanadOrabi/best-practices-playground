namespace HealthMonitor.Domain;

public sealed class Server
{
    public required string Name { get; init; }
    public required string IpAddress { get; init; }
    public required string Type { get; init; } // e.g., "iDRAC", "ILO"
    public required string Username { get; init; }
    public required string Password { get; init; }
}