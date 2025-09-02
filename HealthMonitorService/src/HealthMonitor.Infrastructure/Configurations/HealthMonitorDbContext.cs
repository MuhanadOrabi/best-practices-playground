using HealthMonitor.Domain;
using Microsoft.EntityFrameworkCore;

namespace HealthMonitor.Infrastructure.Configurations;

public class HealthMonitorDbContext : DbContext
{
    public HealthMonitorDbContext(DbContextOptions<HealthMonitorDbContext> options)
        : base(options) { }

    public DbSet<HealthCheckResult> HealthCheckResults => Set<HealthCheckResult>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthMonitorDbContext).Assembly);
    }
}