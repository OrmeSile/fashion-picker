using FileRepository.ConfigurationOptions;
using FileRepository.Entities;
using Microsoft.Extensions.Options;

namespace FileRepository.Services.FileHandlers;

public class ImageHandler : IFileHandler
{
    private readonly StaticPathProvider _staticPathProvider;
    private readonly IOptions<FileRepositoryOptions> _staticFileOptions;
    private readonly IImageOptimizer _imageOptimizer;

    public ImageHandler(
        StaticPathProvider staticPathProvider,
        IOptions<FileRepositoryOptions> staticFileOptions,
        IImageOptimizer imageOptimizer
    )
    {
        _staticPathProvider = staticPathProvider;
        _staticFileOptions = staticFileOptions;
        _imageOptimizer = imageOptimizer;
    }

    public async Task<RepositoryFileInformation> SaveFile(MemoryStream memoryStream, string mimeType)
    {
        if (!memoryStream.CanSeek)
            throw new NotSupportedException($"Stream is not seekable: {memoryStream}");

        memoryStream.Position = 0;


        var extension = GetExtensionStringForMimeType(mimeType);

        var fileName = Guid.NewGuid().ToString("N");

        var (writeOperations, fileNames) = PrepareFile(memoryStream, fileName, extension);

        await SaveFileToDisk(writeOperations);

        var repoFileInfo = new RepositoryFileInformation
        {
            Extension = extension,
            MimeType = mimeType,
            PhysicalFileName = fileName,
            LogicalFileName = null,
            Tags = [],
            PathSmall = fileNames[ImageSize.Small].FileUrl,
            PathMedium = fileNames[ImageSize.Medium].FileUrl,
            PathBig = fileNames[ImageSize.Large].FileUrl,
            PathOriginal = fileNames[ImageSize.Original].FileUrl
        };
        return repoFileInfo;
    }

    private (Dictionary<ImageSize, (byte[] Data, string Path)>, Dictionary<ImageSize, (string FullFileName, string FilePath, string FileUrl)>
        ) PrepareFile(MemoryStream originalImage, string fileName, string extension)
    {
        var fileNames = Enum.GetValues<ImageSize>().Select(size =>
        {
            var (fullFileName, filePath, fileUrl) = GenerateFileInformation(size, fileName, extension);
            return (size, fullFileName, filePath, fileUrl);
        }).ToDictionary(k => k.size, v => (v.fullFileName, v.filePath, v.fileUrl));

        var resizedImages = _imageOptimizer.ResizeImage(originalImage);

        var writeOperations = new Dictionary<ImageSize, (byte[] Data, string Path)>();

        writeOperations[ImageSize.Original] = (resizedImages.Original, fileNames[ImageSize.Original].filePath);

        if (resizedImages.Small != null)
            writeOperations[ImageSize.Small] = (resizedImages.Small, fileNames[ImageSize.Small].filePath);

        if (resizedImages.Medium != null)
            writeOperations[ImageSize.Medium] = (resizedImages.Medium, fileNames[ImageSize.Medium].filePath);

        if (resizedImages.Big != null)
            writeOperations[ImageSize.Large] = (resizedImages.Big, fileNames[ImageSize.Large].filePath);

        return (writeOperations, fileNames);
    }

    private async Task SaveFileToDisk(Dictionary<ImageSize, (byte[] Data, string Path)> writeOperations)
    {
        var fileDict = new Dictionary<ImageSize, FileStream>();
        var fileCopyCancellationTokenSource = new CancellationTokenSource();
        var fileCopyCancellationToken = fileCopyCancellationTokenSource.Token;
        try
        {
            foreach (var operation in writeOperations) fileDict.Add(operation.Key, File.Create(operation.Value.Path));

            await Parallel.ForEachAsync(writeOperations, new ParallelOptions
            {
                CancellationToken = fileCopyCancellationToken
            }, async (operation, token) =>
            {
                using var stream = new MemoryStream(operation.Value.Data);
                await stream.CopyToAsync(fileDict[operation.Key], token);
                stream.Close();
            });
        }
        catch (Exception ex)
        {
            await fileCopyCancellationTokenSource.CancelAsync();
            throw new OperationCanceledException(ex.Message);
        }
    }

    private (string fullFileName, string filePath, string fileUrl) GenerateFileInformation(ImageSize size, string fileIdentifier, string fileExtension)
    {
        var fullFileName = GenerateFullFileName(size, fileIdentifier, fileExtension);

        var absoluteBasePath = _staticPathProvider.GetFilePath();
        var saveLocationFolder = _staticFileOptions.Value.SaveLocation;
        var filePath = GenerateFilePath(absoluteBasePath, saveLocationFolder, fullFileName);
        var fileUrl = GenerateFileUrl(saveLocationFolder, fullFileName);

        return (fullFileName, filePath, fileUrl);
    }

    private string GetExtensionStringForMimeType(string mimeType)
    {
        return mimeType switch
        {
            "image/jpeg" => ".jpg",
            _ => throw new NotSupportedException($"File type is not supported: {mimeType}")
        };
    }

    private string GenerateFullFileName(ImageSize size, string fileIdentifier, string extension)
    {
        return $"{fileIdentifier}-{size.ToString().ToLowerInvariant()}{extension}";
    }

    private string GenerateFilePath(string absoluteBasePath, string saveLocationFolder, string fileName)
    {
        return Path.Combine(absoluteBasePath, saveLocationFolder, fileName);
    }

    private string GenerateFileUrl(string saveLocationFolder, string fileName)
    {
        return $"{saveLocationFolder}/{fileName}";
    }
}

public record ResizedImage(ImageSize size, byte[] image);

public enum ImageSize
{
    Original,
    Large,
    Medium,
    Small
}