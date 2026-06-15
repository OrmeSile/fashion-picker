using FashionPicker.FileRepository.Converters;
using FashionPicker.FileRepository.Providers;
using FashionPicker.FileRepository.Services;
using FileRepository.Api;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FashionPicker.FileRepository.Endpoints;

public static class FileUploadEndpoints
{
    extension(WebApplication app)
    {
        public void MapFileUploadGroup()
        {
            app.MapGroup("").MapUploadEndpoints();
        }
    }

    extension(RouteGroupBuilder group)
    {
        private void MapUploadEndpoints()
        {
            group.MapPost("upload", Upload);
            group.MapGet("", GetAllFileInformation);
            group.MapGet("/{fileIdentifier:guid}", GetAllFileInformation);
        }
    }

    private static async Task<Results<Ok<FileUploadUploadResponse>, BadRequest<string>>> Upload(HttpRequest request, MultipartFileService fileService, HttpContext httpContext, [FromQuery]Guid userId)
    {
        if (!request.ContentType?.StartsWith("multipart/form-data") ?? true)
            return TypedResults.BadRequest("the request does not contain multipart/form-data");

        var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(request.ContentType).Boundary).Value;
        if (string.IsNullOrWhiteSpace(boundary))
            return TypedResults.BadRequest("Missing boundary header");

        var cancellationToken = httpContext.RequestAborted;

        var repositoryFileInformation = await fileService.SaveFiles(boundary, request.Body, userId, cancellationToken);

        var response = new FileUploadUploadResponse(repositoryFileInformation.Select(f => f.ToDto()).ToList());
        return TypedResults.Ok(response);
    }

    private static async Task<Ok<FileUploadGetResponse>> GetAllFileInformation(RepositoryFileInformationsProvider provider)
    {
        var results = await provider.GetAllFileInformations();
        var resultsDto = results.Select(f => f.ToDto());
        return TypedResults.Ok(new FileUploadGetResponse(resultsDto));
    }


    public static async Task<Results<Ok<FileUploadGetFileIdentifierResponse>, NotFound>> GetById(Guid fileIdentifier, RepositoryFileInformationsProvider provider)
    {
        var fileInformation = await provider.GetFileInformationById(fileIdentifier);
        if(fileInformation == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(new FileUploadGetFileIdentifierResponse(fileInformation.ToDto()));
    }
}