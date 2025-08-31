using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using HealthMonitor.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HealthMonitor.Infrastructure.Tests.Integration;

public abstract class TestBase : IAsyncLifetime
{
    private IContainer _dbContainer = null!;

    protected IHost TestHost { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _dbContainer = new ContainerBuilder()
            .WithImage("postgres:16")
            .WithName("healthmonitor-test-db")
            .WithEnvironment("POSTGRES_USER", "healthmonitor")
            .WithEnvironment("POSTGRES_PASSWORD", "secret123")
            .WithEnvironment("POSTGRES_DB", "HealthMonitorDb")
            .WithPortBinding(port: 5432, assignRandomHostPort: false)
            .WithExposedPort(5423)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(port: 5432))
            .Build();

        await _dbContainer.StartAsync();

        TestHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                var dict = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:Default"] = "Host=localhost;Port=5432;Database=HealthMonitorDb;Username=healthmonitor;Password=secret123"
                };
                config.AddInMemoryCollection(dict);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<HealthMonitorDbContext>(options =>
                    options.UseNpgsql(context.Configuration.GetConnectionString("Default")));

                // Register real implementations if needed here
                
            })
            .Build();

        using var scope = TestHost.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<HealthMonitorDbContext>();
        await db.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }
}