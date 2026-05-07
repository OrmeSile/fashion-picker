using FashionPicker.FileRepository.Entities;
using FashionPicker.FileRepository.Services;

namespace FashionPicker.FileRepository.Providers;

public class RepositoryFileInformationsProvider
{
    private readonly FileRepositoryDbContext _context;

    public RepositoryFileInformationsProvider(FileRepositoryDbContext context, IDataConsistencyChecker dataConsistencyChecker)
    {
        _context = context;
    }

    public IEnumerable<RepositoryFileInformation> GetAllFileInformations()
    {
        return _context.RepositoryFileInformations.ToList();
    }

    public async Task<RepositoryFileInformation?> GetFileInformationById(Guid fileId)
    {
        return await _context.RepositoryFileInformations.FindAsync(fileId);
    }
}