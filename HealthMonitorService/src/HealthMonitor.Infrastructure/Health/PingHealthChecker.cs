using HealthMonitor.Domain;
using System.Net.NetworkInformation;

namespace HealthMonitor.Infrastructure.Health;

public class PingHealthChecker : IHealthChecker
{
    public async Task<HealthCheckResult> CheckAsync(Server server, CancellationToken cancellationToken = default)
    {
        var result = HealthCheckResult.Create(server.Name);

        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(server.IpAddress);

            if (reply.Status == IPStatus.Success)
            {
                result.MarkAsHealthy(reply.Status.ToString());
            }
            else
            {
                result.MarkAsUnhealthy(reply.Status.ToString());
            }
        }
        catch (Exception ex)
        {
            result.MarkAsUnhealthy($"Ping failed: {ex.Message}");
        }

        return result;
    }
}