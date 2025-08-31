using HealthMonitor.Domain;
using Microsoft.EntityFrameworkCore;

namespace HealthMonitor.Infrastructure.DbContext;

public class HealthMonitorDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public HealthMonitorDbContext(DbContextOptions<HealthMonitorDbContext> options)
        : base(options) { }

    public DbSet<HealthCheckResult> HealthCheckResults => Set<HealthCheckResult>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("monitoring");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthMonitorDbContext).Assembly);
    }
}