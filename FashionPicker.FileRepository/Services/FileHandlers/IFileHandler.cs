using FashionPicker.FileRepository.Entities;

namespace FashionPicker.FileRepository.Services.FileHandlers;

public interface IFileHandler
{
    public Task<RepositoryFileInformation> SaveFile(MemoryStream memoryStream, string mimeType);
}