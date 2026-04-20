using FileRepository.ConfigurationOptions;
using FileRepository.Entities;
using Microsoft.Extensions.Options;
using MimeDetective;

namespace FileRepository.Services;

public class FileStreamManager : IFileStreamManager
{
    private readonly IContentInspector _contentInspector;
    private readonly FileRepositoryDbContext _dbContext;
    private readonly StaticPathProvider _staticPathProvider;
    private readonly IOptions<FileRepositoryOptions> _staticFileOptions;
    private readonly ImageOptimizer _imageOptimizer;

    public FileStreamManager(
        IContentInspector contentInspector,
        FileRepositoryDbContext dbContext,
        StaticPathProvider staticPathProvider,
        IOptions<FileRepositoryOptions> staticFileOptions,
        ImageOptimizer imageOptimizer
    )
    {
        _staticFileOptions = staticFileOptions;
        _imageOptimizer = imageOptimizer;
        _contentInspector = contentInspector;
        _dbContext = dbContext;
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

        var inspection = _contentInspector.Inspect(memoryStream);
        memoryStream.Position = 0;

        var results = inspection.ByMimeType();

        var fileName = Guid.NewGuid().ToString("N");

        if (results.Length == 0)
            throw new InvalidOperationException("File type is not known.");


        if (!results[0].MimeType.StartsWith("image/"))
            throw new NotImplementedException();

        var smallFileName = $"{fileName}-small";
        var mediumFileName = $"{fileName}-medium";
        var bigFileName = $"{fileName}-big";
        var originalFileName = $"{fileName}-original";

        var resizedImages = _imageOptimizer.ResizeImage(memoryStream);

        if (results[0].MimeType.EndsWith("jpeg"))
        {
            const string fileExtension = "webp";
            smallFileName = $"{smallFileName}.{fileExtension}";
            mediumFileName = $"{mediumFileName}.{fileExtension}";
            bigFileName = $"{bigFileName}.{fileExtension}";
            originalFileName = $"{originalFileName}.{fileExtension}";
        }
        else
            throw new NotSupportedException($"File type is not supported: {results[0].MimeType}");

        var smallImageFilePath = Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, smallFileName);
        var mediumImageFilePath = Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, mediumFileName);
        var bigImageFilePath = Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, bigFileName);
        var originalImageFIlePath =  Path.Combine(_staticPathProvider.GetFilePath(), _staticFileOptions.Value.SaveLocation, originalFileName);
        List<string> pathList = [smallImageFilePath, mediumImageFilePath, bigImageFilePath, originalImageFIlePath];
        var fileDict = new Dictionary<string, FileStream>();

        if (resizedImages.Small != null)
            fileDict.Add(smallImageFilePath, File.Create(smallImageFilePath));
        if(resizedImages.Medium != null)
            fileDict.Add(mediumImageFilePath, File.Create(mediumImageFilePath));
        if(resizedImages.Big != null)
            fileDict.Add(bigImageFilePath, File.Create(bigImageFilePath));
        if(resizedImages.Original != null)
            fileDict.Add(originalImageFIlePath, File.Create(originalImageFIlePath));

        List<MemoryStream> saveStreams = [];

        var fileCopyCancellationTokenSource = new CancellationTokenSource();
        var fileCopyCancellationToken = fileCopyCancellationTokenSource.Token;
        var tasks = new List<Task>();
        try
        {

            tasks.Add(Task.Run(async () =>
            {
                using var stream = new MemoryStream(resizedImages.Small);
                await stream.CopyToAsync(fileDict[smallImageFilePath], fileCopyCancellationToken);
            }, fileCopyCancellationToken));

            tasks.Add(Task.Run(async () =>
            {
                using var originalStream = new MemoryStream(resizedImages.Original);
                await originalStream.CopyToAsync(fileDict[originalImageFIlePath], fileCopyCancellationToken);
            }, fileCopyCancellationToken));

            if (resizedImages.Medium != null)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using var stream = new MemoryStream(resizedImages.Medium);
                    await stream.CopyToAsync(fileDict[mediumImageFilePath], fileCopyCancellationToken);
                }, fileCopyCancellationToken));
            }

            if (resizedImages.Big != null)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using var stream = new MemoryStream(resizedImages.Big);
                    await stream.CopyToAsync(fileDict[bigImageFilePath], fileCopyCancellationToken);
                }, fileCopyCancellationToken));
            }
        }
        catch(Exception ex)
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
            MimeType = results[0].MimeType,
            PhysicalFileName = fileName,
            LogicalFileName = null,
            Tags = [],
            PathSmall = $"{_staticFileOptions.Value.SaveLocation}/{smallFileName}",
            PathMedium =resizedImages.Medium != null ?  $"{_staticFileOptions.Value.SaveLocation}/{mediumFileName}": null,
            PathBig = resizedImages.Big != null ?  $"{_staticFileOptions.Value.SaveLocation}/{bigFileName}": null,
            PathOriginal = $"{_staticFileOptions.Value.SaveLocation}/{originalFileName}"
        };
        return repoFileInfo;
    }
}

public interface IFileStreamManager
{
    Task<RepositoryFileInformation> SaveFile(Stream contentStream, CancellationToken cancellationToken);
}