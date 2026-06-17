using FashionPicker.FileRepository.Configuration;
using FashionPicker.FileRepository.Entities;
using FashionPicker.FileRepository.Interfaces;
using FashionPicker.FileRepository.Providers;
using Microsoft.Extensions.Options;

namespace FashionPicker.FileRepository.Services.FileHandlers;

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
            PathMedium = fileNames.GetValueOrDefault(ImageSize.Medium).FileUrl,
            PathBig = fileNames.GetValueOrDefault(ImageSize.Large).FileUrl,
            PathOriginal = fileNames[ImageSize.Original].FileUrl
        };
        return repoFileInfo;
    }

    private (Dictionary<ImageSize, (byte[] Data, string Path)>, Dictionary<ImageSize, (string FullFileName, string FilePath, string FileUrl)>
        ) PrepareFile(MemoryStream originalImage, string fileName, string extension)
    {
        var absoluteBasePath = _staticPathProvider.GetFilePath();
        var saveLocationFolder = Path.Combine(absoluteBasePath, _staticFileOptions.Value.SaveLocation, fileName);

        Directory.CreateDirectory(saveLocationFolder);

        var resizedImages = _imageOptimizer.ResizeImage(originalImage);

        var fileNames = new Dictionary<ImageSize, (string FullFileName, string FilePath, string FileUrl)>();

        var writeOperations = new Dictionary<ImageSize, (byte[] Data, string Path)>();

        var (originalFullFileName, originalFilePath, originalFileUrl) = GenerateFileInformation(ImageSize.Original, fileName, extension, saveLocationFolder);
        fileNames[ImageSize.Original] = (originalFullFileName, originalFilePath, originalFileUrl);
        writeOperations[ImageSize.Original] = (resizedImages.Original, originalFilePath);

        if (resizedImages.Small != null)
        {
            var (fullFileName, filePath, fileUrl) = GenerateFileInformation(ImageSize.Small, fileName, extension, saveLocationFolder);
            fileNames[ImageSize.Small] = (fullFileName, filePath, fileUrl);
            writeOperations[ImageSize.Small] = (resizedImages.Small, filePath);
        }

        if (resizedImages.Medium != null)
        {
            var (fullFileName, filePath, fileUrl) = GenerateFileInformation(ImageSize.Medium, fileName, extension, saveLocationFolder);
            fileNames[ImageSize.Medium] = (fullFileName, filePath, fileUrl);
            writeOperations[ImageSize.Medium] = (resizedImages.Medium, filePath);
        }

        if (resizedImages.Big != null)
        {
            var (fullFileName, filePath, fileUrl) = GenerateFileInformation(ImageSize.Large, fileName, extension, saveLocationFolder);
            fileNames[ImageSize.Large] = (fullFileName, filePath, fileUrl);
            writeOperations[ImageSize.Large] = (resizedImages.Big, filePath);
        }

        return (writeOperations, fileNames);
    }

    private async Task SaveFileToDisk(Dictionary<ImageSize, (byte[] Data, string Path)> writeOperations)
    {
        var fileDict = new Dictionary<ImageSize, FileStream>();
        var fileCopyCancellationTokenSource = new CancellationTokenSource();
        var fileCopyCancellationToken = fileCopyCancellationTokenSource.Token;
        try
        {
            foreach (var operation in writeOperations)
                fileDict.Add(operation.Key, File.Create(operation.Value.Path));

            await Parallel.ForEachAsync(writeOperations, new ParallelOptions
            {
                CancellationToken = fileCopyCancellationToken
            }, async (operation, token) =>
            {
                using var stream = new MemoryStream(operation.Value.Data);
                await stream.CopyToAsync(fileDict[operation.Key], token);
            });
        }
        catch (Exception ex)
        {
            await fileCopyCancellationTokenSource.CancelAsync();
            throw new OperationCanceledException(ex.Message);
        }
        finally
        {
            foreach (var stream in fileDict.Values)
                await stream.DisposeAsync();
            fileDict.Clear();
        }
    }

    private (string fullFileName, string filePath, string fileUrl) GenerateFileInformation(ImageSize size, string fileIdentifier, string fileExtension, string saveLocationFolder)
    {
        var fullFileName = GenerateFullFileName(size, fileIdentifier, fileExtension);
        var filePath = GenerateFilePath(saveLocationFolder, fullFileName);
        var fileUrl = GenerateFileUrl( fileIdentifier, fullFileName);

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

    private string GenerateFilePath(string saveLocationFolder, string fileName)
    {
        return Path.Combine(saveLocationFolder, fileName);
    }

    private string GenerateFileUrl(string fileIdentifier, string fileName)
    {
        return $"{_staticFileOptions.Value.SaveLocation}/{fileIdentifier}/{fileName}";
    }
}

public enum ImageSize
{
    Original,
    Large,
    Medium,
    Small
}