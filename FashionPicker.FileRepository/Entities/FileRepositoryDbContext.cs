using Microsoft.EntityFrameworkCore;

namespace FileRepository.Entities;

public class FileRepositoryDbContext(DbContextOptions<FileRepositoryDbContext> options): DbContext(options)
{
    public DbSet<RepositoryFileInformation> RepositoryFileInformations { get; set; }
    public DbSet<DataConsistencyCheckEpisode> DataConsistencyCheckEpisodes { get; set; }
}