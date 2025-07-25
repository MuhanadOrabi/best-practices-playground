using HealthMonitor.Domain;

namespace HealthMonitor.Application;

// Composition of behavior via injected interfaces (DIP)
// The service class doesn’t care about how checks are performed or results are saved
// Easy to mock for unit testing
public class HealthCheckService
{
    private readonly IHealthChecker _healthChecker;
    private readonly IHealthCheckResultWriter _resultWriter;

    public HealthCheckService(
        IHealthChecker healthChecker,
        IHealthCheckResultWriter resultWriter)
    {
        _healthChecker = healthChecker;
        _resultWriter = resultWriter;
    }

    public async Task CheckAndRecordAsync(Server server, CancellationToken cancellationToken = default)
    {
        var result = await _healthChecker.CheckAsync(server, cancellationToken);
        await _resultWriter.WriteAsync(result, cancellationToken);
    }
}