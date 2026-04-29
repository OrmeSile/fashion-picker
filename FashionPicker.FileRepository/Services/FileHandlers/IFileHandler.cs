using FileRepository.Entities;

namespace FileRepository.Services.FileHandlers;

public interface IFileHandler
{
    public Task<RepositoryFileInformation> SaveFile(MemoryStream memoryStream, string mimeType);
}