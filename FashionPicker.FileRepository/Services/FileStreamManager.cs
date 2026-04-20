using FashionPicker.FileRepository.Interfaces;
using FileRepository.ConfigurationOptions;
using FileRepository.Entities;
using Microsoft.Extensions.Options;

namespace FileRepository.Services;

public class FileStreamManager : IFileStreamManager
{
    private readonly ISimpleContentInspector _contentInspector;
    private readonly StaticPathProvider _staticPathProvider;
    private readonly IOptions<FileRepositoryOptions> _staticFileOptions;
    private readonly IImageOptimizer _imageOptimizer;

    public FileStreamManager(
        ISimpleContentInspector contentInspector,
        FileRepositoryDbContext dbContext,
        StaticPathProvider staticPathProvider,
        IOptions<FileRepositoryOptions> staticFileOptions,
        IImageOptimizer imageOptimizer
    )
    {
        _staticFileOptions = staticFileOptions;
        _imageOptimizer = imageOptimizer;
        _contentInspector = contentInspector;
        _staticPathProvider = staticPathProvider;
    }

    private const int BUFFER_SIZE = 16 * 64 * 1024;

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

        var fileName = Guid.NewGuid().ToString("N");
        var extension = GetExtensionStringForMimeType(mimeTypeMatches[0].MimeType);

        var smallFileName = $"{fileName}-small.{extension}";
        var mediumFileName = $"{fileName}-medium.{extension}";
        var bigFileName = $"{fileName}-big.{extension}";
        var originalFileName = $"{fileName}-original.{extension}";

        var smallImageFilePath = Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, smallFileName);
        var mediumImageFilePath = Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, mediumFileName);
        var bigImageFilePath = Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, bigFileName);
        var originalImageFIlePath = Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, originalFileName);

        var resizedImages = _imageOptimizer.ResizeImage(memoryStream);

        List<string> pathList = [smallImageFilePath, mediumImageFilePath, bigImageFilePath, originalImageFIlePath];
        var fileDict = new Dictionary<string, FileStream>();

        fileDict.Add(originalImageFIlePath, File.Create(originalImageFIlePath));

        if (resizedImages.Small != null)
            fileDict.Add(smallImageFilePath, File.Create(smallImageFilePath));

        if (resizedImages.Medium != null)
            fileDict.Add(mediumImageFilePath, File.Create(mediumImageFilePath));

        if (resizedImages.Big != null)
            fileDict.Add(bigImageFilePath, File.Create(bigImageFilePath));

        var fileCopyCancellationTokenSource = new CancellationTokenSource();
        var fileCopyCancellationToken = fileCopyCancellationTokenSource.Token;
        var tasks = new List<Task>();
        try
        {
            tasks.Add(Task.Run(async () =>
            {
                using var originalStream = new MemoryStream(resizedImages.Original);
                await originalStream.CopyToAsync(fileDict[originalImageFIlePath], fileCopyCancellationToken);
            }, fileCopyCancellationToken));

            if (resizedImages.Small != null)
                tasks.Add(Task.Run(async () =>
                {
                    using var stream = new MemoryStream(resizedImages.Small);
                    await stream.CopyToAsync(fileDict[smallImageFilePath], fileCopyCancellationToken);
                }, fileCopyCancellationToken));

            if (resizedImages.Medium != null)
                tasks.Add(Task.Run(async () =>
                {
                    using var stream = new MemoryStream(resizedImages.Medium);
                    await stream.CopyToAsync(fileDict[mediumImageFilePath], fileCopyCancellationToken);
                }, fileCopyCancellationToken));

            if (resizedImages.Big != null)
                tasks.Add(Task.Run(async () =>
                {
                    using var stream = new MemoryStream(resizedImages.Big);
                    await stream.CopyToAsync(fileDict[bigImageFilePath], fileCopyCancellationToken);
                }, fileCopyCancellationToken));
        }
        catch (Exception ex)
        {
            await fileCopyCancellationTokenSource.CancelAsync();
            throw new OperationCanceledException(ex.Message);
        }

        await Task.WhenAll(tasks);

        foreach (var keyValuePair in fileDict)
            keyValuePair.Value.Close();

        var repoFileInfo = new RepositoryFileInformation
        {
            Extension = "jpg",
            MimeType = mimeTypeMatches[0].MimeType,
            PhysicalFileName = fileName,
            LogicalFileName = null,
            Tags = [],
            PathSmall = $"{_staticFileOptions.Value.SaveLocation}/{smallFileName}",
            PathMedium = resizedImages.Medium != null ? $"{_staticFileOptions.Value.SaveLocation}/{mediumFileName}" : null,
            PathBig = resizedImages.Big != null ? $"{_staticFileOptions.Value.SaveLocation}/{bigFileName}" : null,
            PathOriginal = $"{_staticFileOptions.Value.SaveLocation}/{originalFileName}"
        };
        return repoFileInfo;
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