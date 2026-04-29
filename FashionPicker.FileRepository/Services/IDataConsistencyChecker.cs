using FileRepository.Entities;
using FileRepository.Interfaces;

namespace FileRepository.Services;

public interface IDataConsistencyChecker
{
    void EnsureDataConsistency<TSet>() where TSet : class, IPhysicalFile;
}

public class DataConsistencyChecker : IDataConsistencyChecker
{
    private readonly FileRepositoryDbContext _context;
    private readonly StaticPathProvider _staticPathProvider;

    public DataConsistencyChecker(
        FileRepositoryDbContext context,
        StaticPathProvider staticPathProvider
    )
    {
        _context = context;
        _staticPathProvider = staticPathProvider;
    }

    public void EnsureDataConsistency<TSet>() where TSet : class, IPhysicalFile
    {
        var basePath = _staticPathProvider.GetFilePath();

        var episode = new DataConsistencyCheckEpisode();
        foreach (var file in _context.Set<TSet>())
            if (File.Exists(Path.Combine(basePath, file.PathOriginal)))
            {
                episode.FilesValidated++;
            }
            else
            {
                _context.Set<TSet>().Remove(file);
                episode.FilesDeleted++;
            }

        _context.SaveChanges();
    }
}