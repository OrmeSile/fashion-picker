using FashionPicker.FileRepository.Interfaces;
using FileRepository.Entities;
using FileRepository.Services.FileHandlers;

namespace FileRepository.Services;

public class FileStreamManager : IFileStreamManager
{
    private readonly ISimpleContentInspector _contentInspector;
    private readonly ImageHandler _imageHandler;

    public FileStreamManager(
        ISimpleContentInspector contentInspector,
        ImageHandler imageHandler
    )
    {
        _contentInspector = contentInspector;
        _imageHandler = imageHandler;
    }

    public async Task<RepositoryFileInformation> SaveFile
    (
        Stream contentStream,
        CancellationToken cancellationToken
    )
    {
        var memoryStream = new MemoryStream();
        await contentStream.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;

        var mimeTypeMatches = _contentInspector.Inspect(memoryStream);

        if (mimeTypeMatches.Length == 0)
            throw new InvalidOperationException("File type is not known.");

        if (!mimeTypeMatches[0].MimeType.StartsWith("image/"))
            throw new NotImplementedException();

        return await _imageHandler.SaveFile(memoryStream, mimeTypeMatches[0].MimeType);
    }


    private string GetExtensionStringForMimeType(string mimeType)
    {
        return mimeType switch
        {
            "image/jpeg" => ".jpg",
            _ => throw new NotSupportedException($"File type is not supported: {mimeType}")
        };
    }
}

public interface IFileStreamManager
{
    Task<RepositoryFileInformation> SaveFile(Stream contentStream, CancellationToken cancellationToken);
}