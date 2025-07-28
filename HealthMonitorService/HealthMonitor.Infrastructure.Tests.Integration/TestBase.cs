using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HealthMonitor.Infrastructure.Tests.Integration;

public abstract class TestBase : IAsyncLifetime
{
    protected IHost TestHost { get; private set; } = null!;
    protected IContainer _dbContainer = null!;

    public async Task InitializeAsync()
    {
        var _dbContainer = new ContainerBuilder()
            .WithImage("postgres:16")
            .WithEnvironment("POSTGRES_USER", "healthmonitor")
            .WithEnvironment("POSTGRES_PASSWORD", "secret123")
            .WithEnvironment("POSTGRES_DB", "HealthMonitorDb")
            .WithPortBinding(port: 5432, assignRandomHostPort: true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(port: 5432))
            .Build();

        await _dbContainer.StartAsync();

        TestHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                var dict = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:Default"] = _dbContainer.ConnectionString
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