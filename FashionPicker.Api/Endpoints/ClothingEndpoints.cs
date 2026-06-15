using System.Text.Json;
using FashionPicker.Api.Converters;
using FashionPicker.Api.Dto.Inbound.ClothingRequest;
using FashionPicker.Api.Dto.Outbound.ClothingResponse;
using FashionPicker.Core.Adapters;
using FashionPicker.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FashionPicker.Api.Endpoints;

public static class ClothingEndpoints
{
    extension(WebApplication app)
    {
        public void MapClothingApiGroup()
        {
            app.MapGroup("clothing").MapClothingEndpoints();
        }
    }

    extension(RouteGroupBuilder group)
    {
        private void MapClothingEndpoints()
        {
            group.MapGet("/", GetAllClothing);
            group.MapPost("/", CreateClothing);
        }
    }

    private static async Task<Results<Ok<ClothingGetAllResponse>, NotFound>> GetAllClothing(IClothingRepository clothingRepository, [FromQuery] Guid userId)
    {
        var clothing = await clothingRepository.GetAllForUser(userId);
        var outfitDtoList = clothing.Select(c => c.ToDto()).ToList();
        var response = new ClothingGetAllResponse(outfitDtoList);
        return TypedResults.Ok(response);
    }

    private static async Task<Results<Ok<ClothingMetadataResponse>, BadRequest<string>, InternalServerError>> CreateClothing(
        HttpRequest request,
        HttpContext httpContext,
        ICmsAdapter cmsAdapter,
        IClothingRepository clothingRepository,
        [FromQuery] Guid userId
    )
    {
        if (!request.ContentType?.StartsWith("multipart/form-data") ?? true)
            return TypedResults.BadRequest("the request does not contain multipart/form-data");

        if (!MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaType))
            return TypedResults.BadRequest("missing or malformed content type in request");

        var boundary = HeaderUtilities.RemoveQuotes(mediaType.Boundary).Value;
        if(boundary == null)
            return TypedResults.BadRequest("missing or malformed content type in request");

        var reader = new MultipartReader(boundary, request.Body);

        var cancellationToken = httpContext.RequestAborted;

        using var multipartFormData = new MultipartFormDataContent();

        using var metadataStream = new MemoryStream();

        try
        {
            while (await reader.ReadNextSectionAsync(cancellationToken) is { } section)
            {
                var contentDisposition = section.GetContentDispositionHeader();

                if (contentDisposition == null || !contentDisposition.IsFileDisposition())
                    throw new InvalidOperationException("missing content disposition.");

                if (!MediaTypeHeaderValue.TryParse(section.ContentType, out var sectionType))
                    throw new InvalidOperationException("Invalid content type in section: " + section.ContentType);
                if (sectionType.MediaType == "application/json")
                {
                    await section.Body.CopyToAsync(metadataStream, cancellationToken);
                    metadataStream.Position = 0;
                }
                else
                {
                    var ms = new MemoryStream();
                    await section.Body.CopyToAsync(ms, cancellationToken);
                    ms.Position = 0;
                    var streamContent = new StreamContent(ms);
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(sectionType.MediaType.ToString());

                    multipartFormData.Add(streamContent, contentDisposition.Name.ToString(), contentDisposition.FileName.ToString());
                }
            }
        }
        catch (InvalidOperationException e)
        {
            return TypedResults.BadRequest(e.Message);
        }
        catch (IOException e)
        {
            _ = e;
            return TypedResults.InternalServerError();
        }
        catch (OperationCanceledException e)
        {
            return TypedResults.BadRequest(e.Message);
        }

        var metadata = await ParseInboundJsonMetadata(metadataStream, cancellationToken) ?? throw new InvalidOperationException("missing metadata");

        var fileInformation = await cmsAdapter.UploadFileAsync(multipartFormData, userId);

        var clothing = metadata.ToModel();
        clothing.AddImages(fileInformation);
        clothing.UserId = userId;

        var savedClothing = await clothingRepository.AddClothing(clothing);

        return TypedResults.Ok(savedClothing.ToDto());
    }

    private static async Task<ClothingPostRequestMetadata?> ParseInboundJsonMetadata(Stream bodySection, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await JsonSerializer.DeserializeAsync<ClothingPostRequestMetadata>(
            bodySection,
            options,
            cancellationToken);
    }
}