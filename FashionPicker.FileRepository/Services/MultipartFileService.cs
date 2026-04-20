using System.Text.Json;
using FileRepository.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FileRepository.Services;

public class MultipartFileService(IFileStreamManager streamManager, FileRepositoryDbContext dbContext)
{
    public async Task<List<RepositoryFileInformation>> SaveFiles(string boundary, Stream contentStream, CancellationToken cancellationToken)
    {
        var reader = new MultipartReader(boundary, contentStream);

        FileMetadata? fileMetadata = null;

        var ms =  new MemoryStream();
        string? currentFileName = null;
        List<RepositoryFileInformation> repositoryFilesInformation = [];

        var tasks =  new List<Task>();

        while (await reader.ReadNextSectionAsync(cancellationToken) is { } section)
        {
            var contentDisposition = section.GetContentDispositionHeader();

            if (contentDisposition == null || !contentDisposition.IsFileDisposition())
                throw new InvalidOperationException("missing content disposition.");

            if(!MediaTypeHeaderValue.TryParse(section.ContentType, out var sectionType))
                throw new InvalidOperationException("Invalid content type in section " + section.ContentType);

            if (sectionType.MediaType == "application/json")
            {
                var metadata = await ParseJsonMetadata(section.Body, cancellationToken);

                fileMetadata ??= metadata ?? throw new InvalidOperationException("missing metadata.");
            }
            else
            {
                if (contentDisposition.Name != currentFileName )
                {
                    if (currentFileName != null)
                    {
                        ms.Position = 0;
                        var tempStream = new MemoryStream();
                        await ms.CopyToAsync(tempStream, cancellationToken);
                        tempStream.Position = 0;
                        await ms.FlushAsync(cancellationToken);
                        ms.Position = 0;
                        tasks.Add(Task.Run(async () =>
                        {
                            var entity = await streamManager.SaveFile(tempStream, cancellationToken);
                            entity.LogicalFileName = fileMetadata?.FileName;
                            repositoryFilesInformation.Add(entity);
                        }, cancellationToken));
                    }

                    currentFileName = contentDisposition.Name.ToString();
                }

                await section.Body.CopyToAsync(ms, CancellationToken.None);
            }
        }

        ms.Position = 0;
        tasks.Add(Task.Run( async ()=>
        {
            var lastEntity = await streamManager.SaveFile(ms, cancellationToken);
            lastEntity.LogicalFileName = fileMetadata?.FileName;
            repositoryFilesInformation.Add(lastEntity);
        }));

        Task.WaitAll(tasks, cancellationToken);

        dbContext.RepositoryFileInformations.AddRange(repositoryFilesInformation);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        return repositoryFilesInformation;
    }

    private async Task<FileMetadata?> ParseJsonMetadata(Stream stream, CancellationToken cancellationToken)
    {
        return await JsonSerializer.DeserializeAsync<FileMetadata>(
            stream,
            cancellationToken: cancellationToken);
    }

    public record FileMetadata(string? FileName, string? Section, string[]? Tags);
}