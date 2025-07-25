namespace HealthMonitor.Domain;

public class Server
{
    public string Name { get; init; }
    public string IpAddress { get; init; }
    public string Type { get; init; } // e.g., "iDRAC", "ILO"
    public string Username { get; init; }
    public string Password { get; init; }
}