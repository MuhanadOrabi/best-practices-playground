using HealthMonitor.Application;
using HealthMonitor.Infrastructure;
using Microsoft.Extensions.Configuration;
using HealthMonitor.Console.Entities;
using HealthMonitor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var hostBuilder = Host.CreateDefaultBuilder(args);
    
hostBuilder.UseSerilog((context, services, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});

hostBuilder.ConfigureAppConfiguration(config =>
{
    config.AddJsonFile("appsettings.json");
});
    
hostBuilder.ConfigureServices((context, services) =>
{
    services.Configure<ServerConfig>(context.Configuration);
    services.AddSingleton<IHealthChecker, FakeHealthChecker>();
    services.AddScoped<IHealthCheckResultWriter, DbHealthCheckResultWriter>();

    services.AddDbContext<HealthMonitorDbContext>(options =>
        options.UseSqlServer(context.Configuration.GetConnectionString("Default")));

    services.AddScoped<HealthCheckService>();
});

var host = hostBuilder.Build();

var servers = host.Services
    .GetRequiredService<IConfiguration>()
    .GetSection("Servers")
    .Get<List<Server>>();

var service = host.Services.GetRequiredService<HealthCheckService>();

foreach (var server in servers!)
{
    await service.CheckAndRecordAsync(server);
}
