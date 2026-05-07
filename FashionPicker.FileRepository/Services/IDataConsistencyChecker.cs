using FashionPicker.FileRepository.Entities;
using FashionPicker.FileRepository.Interfaces;
using FashionPicker.FileRepository.Providers;
#pragma warning disable CA2254

namespace FashionPicker.FileRepository.Services;

public interface IDataConsistencyChecker
{
    void EnsureDataConsistency<TSet>(object? state) where TSet : class, IPhysicalFile;
}

public class DataConsistencyChecker : IDataConsistencyChecker
{
    private readonly StaticPathProvider _staticPathProvider;
    private readonly ILogger<DataConsistencyChecker> _logger;
    private readonly IServiceScopeFactory _service;

    public DataConsistencyChecker
    (
        StaticPathProvider staticPathProvider,
        ILogger<DataConsistencyChecker> logger,
        IServiceScopeFactory service
    )
    {
        _staticPathProvider = staticPathProvider;
        _logger = logger;
        _service = service;
    }

    public async void EnsureDataConsistency<TSet>(object? state) where TSet : class, IPhysicalFile
    {
        try
        {
            using var scope = _service.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FileRepositoryDbContext>();
            await context.Database.EnsureCreatedAsync();

            _logger.LogInformation($"Starting data consistency episode at {DateTime.Now}");
            var basePath = _staticPathProvider.GetFilePath();

            var episode = new DataConsistencyCheckEpisode();
            foreach (var file in context.Set<TSet>())
                if (File.Exists(Path.Combine(basePath, file.PathOriginal)))
                {
                    episode.FilesValidated++;
                }
                else
                {
                    context.Set<TSet>().Remove(file);
                    episode.FilesDeleted++;
                }
            episode.TimeOfCheck = DateTime.UtcNow;

            context.Add(episode);
            await context.SaveChangesAsync();

            _logger.LogInformation($"Ended data consistency episode at {DateTime.Now}. {episode.FilesValidated} files validated. {episode.FilesDeleted} files deleted.");
        }
        catch (Exception e)
        {
            _logger.LogError($"error: {e.Message}");
        }
    }
}