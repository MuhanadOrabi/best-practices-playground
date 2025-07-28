using Microsoft.EntityFrameworkCore;
using HealthMonitor.Domain;

namespace HealthMonitor.Infrastructure;

public class HealthMonitorDbContext : DbContext
{
    public HealthMonitorDbContext(DbContextOptions<HealthMonitorDbContext> options)
        : base(options) { }

    public DbSet<HealthCheckResult> HealthCheckResults => Set<HealthCheckResult>();
}