using FileRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileRepository;

public static class DbContextProvider
{
    public static IServiceCollection AddFileRepositoryDbContext(this IServiceCollection services, string? connectionString)
    {
        if (connectionString == null)
            throw new NullReferenceException(nameof(connectionString));

        services.AddDbContextPool<FileRepositoryDbContext>(opt =>
            opt.UseNpgsql(connectionString)
        );

        return services;
    }
}