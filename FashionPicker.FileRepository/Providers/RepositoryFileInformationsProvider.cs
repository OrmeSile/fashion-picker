using FashionPicker.FileRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.FileRepository.Providers;

public class RepositoryFileInformationsProvider
{
    private readonly FileRepositoryDbContext _context;

    public RepositoryFileInformationsProvider(FileRepositoryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RepositoryFileInformation>> GetAllFileInformations()
    {
        return await _context.RepositoryFileInformations.ToListAsync();
    }

    public async Task<RepositoryFileInformation?> GetFileInformationById(Guid fileId)
    {
        return await _context.RepositoryFileInformations.FindAsync(fileId);
    }
}