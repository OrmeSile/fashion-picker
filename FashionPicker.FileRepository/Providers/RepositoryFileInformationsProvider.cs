using FileRepository.Entities;
using FileRepository.Services;

namespace FileRepository;

public class RepositoryFileInformationsProvider
{
    private readonly FileRepositoryDbContext _context;
    private readonly IDataConsistencyChecker _dataConsistencyChecker;

    public RepositoryFileInformationsProvider(FileRepositoryDbContext context, IDataConsistencyChecker dataConsistencyChecker)
    {
        _context = context;
        _dataConsistencyChecker = dataConsistencyChecker;
    }

    public IEnumerable<RepositoryFileInformation> GetAllFileInformations()
    {
        _dataConsistencyChecker.EnsureDataConsistency<RepositoryFileInformation>();
        return _context.RepositoryFileInformations.ToList();
    }
}