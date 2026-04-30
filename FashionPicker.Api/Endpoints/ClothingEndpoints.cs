using System.Text.Json;
using FashionPicker.Api.Converters;
using FashionPicker.Api.Dto.Inbound.ClothingRequest;
using FashionPicker.Api.Dto.Outbound.ClothingResponse;
using FashionPicker.Core.Adapters;
using FashionPicker.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private RouteGroupBuilder MapClothingEndpoints()
        {
            group.MapGet("/", GetAllClothing);
            group.MapPost("/", CreateClothing);
            return group;
        }
    }

    private static async Task<Results<Ok<ClothingGetResponse>, NotFound>> GetAllClothing(IClothingRepository clothingRepository)
    {
        var clothing = await clothingRepository.GetAll();
        var outfitDtos = clothing.Select(c => c.ToDto()).ToList();
        var response = new ClothingGetResponse(outfitDtos);
        return TypedResults.Ok(response);
    }

    private static async Task<Results<Ok<ClothingMetadataResponse>, BadRequest<string>>> CreateClothing(
        HttpRequest request,
        HttpContext httpContext,
        ICmsAdapter cmsAdapter,
        IClothingRepository clothingRepository
    )
    {
        if (!request.ContentType?.StartsWith("multipart/form-data") ?? true)
            return TypedResults.BadRequest("the request does not contain multipart/form-data");

        var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(request.ContentType).Boundary).Value;
        if (string.IsNullOrWhiteSpace(boundary))
            return TypedResults.BadRequest("Missing boundary header");

        var reader = new MultipartReader(boundary, request.Body);

        var cancellationToken = httpContext.RequestAborted;

        var multipartFormData = new MultipartFormDataContent();

        var metadataStream = new MemoryStream();
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

        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }

        var metadata = await ParseInboundJsonMetadata(metadataStream, cancellationToken) ?? throw new InvalidOperationException("missing metadata");

        var fileInformation = await cmsAdapter.UploadFileAsync(multipartFormData);

        var clothing = metadata.ToModel();
        clothing.AddImages(fileInformation);

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