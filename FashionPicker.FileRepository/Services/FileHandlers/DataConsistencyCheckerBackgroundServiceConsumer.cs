using FashionPicker.FileRepository.Entities;

namespace FashionPicker.FileRepository.Services.FileHandlers;

public class DataConsistencyCheckerBackgroundServiceConsumer: BackgroundService
{
    private Timer? _timer;
    private IServiceProvider Services { get; }

    public DataConsistencyCheckerBackgroundServiceConsumer(IServiceProvider services)
    {
        Services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ExecuteDataConsistencyCheck(stoppingToken);
    }

    private Task ExecuteDataConsistencyCheck(ValueType stoppingToken)
    {
        using var scope = Services.CreateScope();
        var dataConsistencyChecker = scope.ServiceProvider.GetRequiredService<IDataConsistencyChecker>();

        _timer = new Timer(dataConsistencyChecker.EnsureDataConsistency<RepositoryFileInformation>, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return base.StopAsync(cancellationToken);
    }
}