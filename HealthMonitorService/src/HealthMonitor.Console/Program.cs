using HealthMonitor.Application;
using HealthMonitor.Infrastructure;
using Microsoft.Extensions.Configuration;
using HealthMonitor.Console.Entities;
using HealthMonitor.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var hostBuilder = WebApplication.CreateBuilder(args);
    
hostBuilder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

hostBuilder.Configuration.AddJsonFile("appsettings.json");
    
hostBuilder.Host.ConfigureServices((context, services) =>
{
    services.Configure<ServerConfig>(context.Configuration);
    services.AddSingleton<IHealthChecker, FakeHealthChecker>();
    services.AddScoped<IHealthCheckResultWriter, ConsoleResultWriter>();

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
