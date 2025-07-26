using HealthMonitor.Application;
using HealthMonitor.Infrastructure;
using Microsoft.Extensions.Configuration;
using HealthMonitor.Console.Entities;

// Load configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// Option A: Bind to custom ServerConfig class
var serverConfig = new ServerConfig();
config.Bind(serverConfig);
var servers = serverConfig.Servers;

// Option B: Bind directly
// var servers = config.GetSection("Servers").Get<List<Server>>();

// Compose application
var checker = new FakeHealthChecker();
var writer = new ConsoleResultWriter();
var service = new HealthCheckService(checker, writer);

foreach (var server in servers)
{
    await service.CheckAndRecordAsync(server);
}