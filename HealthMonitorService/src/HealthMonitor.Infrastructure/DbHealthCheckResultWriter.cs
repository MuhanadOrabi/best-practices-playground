using HealthMonitor.Domain;
using HealthMonitor.Infrastructure.DbContext;
using Microsoft.Extensions.Logging;

namespace HealthMonitor.Infrastructure;

public class DbHealthCheckResultWriter : IHealthCheckResultWriter
{
    private readonly HealthMonitorDbContext _db;
    private readonly ILogger<DbHealthCheckResultWriter> _logger;

    public DbHealthCheckResultWriter(HealthMonitorDbContext db, ILogger<DbHealthCheckResultWriter> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task WriteAsync(HealthCheckResult result, CancellationToken cancellationToken = default)
    {
        try
        {
            _db.HealthCheckResults.Add(result);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Successfully saved health check result for {Server}", result.ServerName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write health check result for {Server}", result.ServerName);
        }
    }
}