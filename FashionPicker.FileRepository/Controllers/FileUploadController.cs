using FileRepository.Api;
using FileRepository.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FileRepository;

[ApiController]
[Route("")]
public class FileUploadController: ControllerBase
{
    private readonly MultipartFileService _fileService;
    private readonly RepositoryFileInformationsProvider _repositoryFileInformationsProvider;

    public FileUploadController(
        MultipartFileService fileService,
        RepositoryFileInformationsProvider repositoryFileInformationsProvider
        )
    {
        _fileService = fileService;
        _repositoryFileInformationsProvider = repositoryFileInformationsProvider;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<Results<Ok<FileUploadUploadResponse>,BadRequest<string>>> Upload()
    {
        if (!Request.ContentType?.StartsWith("multipart/form-data") ?? true)
            return TypedResults.BadRequest( "the request does not contain multipart/form-data");

        var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
        if(string.IsNullOrWhiteSpace(boundary))
            return TypedResults.BadRequest("Missing boundary header");

        var cancellationToken = HttpContext.RequestAborted;

        var repositoryFileInformation = await _fileService.SaveFiles(boundary, Request.Body, cancellationToken);
        return TypedResults.Ok(new FileUploadUploadResponse(repositoryFileInformation.Select(fileInformation => fileInformation.ToDto()).ToList()));
    }

    [HttpGet]
    public Ok<FileUploadGetResponse> Get()
    {
        return TypedResults.Ok(new FileUploadGetResponse
            (
                Data: _repositoryFileInformationsProvider.GetAllFileInformations()
                    .Select(x => x.ToDto())
                )
        );
    }
}


