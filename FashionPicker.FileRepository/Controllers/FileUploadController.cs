using FashionPicker.FileRepository.Converters;
using FashionPicker.FileRepository.Providers;
using FashionPicker.FileRepository.Services;
using FileRepository.Api;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FashionPicker.FileRepository.Controllers;

[ApiController]
[Route("")]
public class FileUploadController : ControllerBase
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
    public async Task<Results<Ok<FileUploadUploadResponse>, BadRequest<string>>> Upload()
    {
        if (!Request.ContentType?.StartsWith("multipart/form-data") ?? true)
            return TypedResults.BadRequest("the request does not contain multipart/form-data");

        var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
        if (string.IsNullOrWhiteSpace(boundary))
            return TypedResults.BadRequest("Missing boundary header");

        var cancellationToken = HttpContext.RequestAborted;

        var repositoryFileInformation = await _fileService.SaveFiles(boundary, Request.Body, cancellationToken);

        var response = new FileUploadUploadResponse(repositoryFileInformation.Select(f => f.ToDto()).ToList());
        return TypedResults.Ok(response);
    }

    [HttpGet]
    public Ok<FileUploadGetResponse> Get()
    {
        return TypedResults.Ok(new FileUploadGetResponse
            (
                _repositoryFileInformationsProvider.GetAllFileInformations()
                    .Select(x => x.ToDto())
            )
        );
    }
}