using System.Text.Json;
using FashionPicker.Api.Configuration;
using FashionPicker.Api.Converters;
using FashionPicker.Api.Dto.Inbound.OutfitRequest;
using FashionPicker.Api.Dto.Outbound.OutfitResponse;
using FashionPicker.Core.Adapters;
using FashionPicker.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FashionPicker.Api.Endpoints;

public static class OutfitEndpoints
{
    extension(WebApplication app)
    {
        public void MapOutfitApiGroup()
        {
            app.MapGroup("outfit").MapOutfitEndpoints();
        }
    }

    extension(RouteGroupBuilder group)
    {
        private void MapOutfitEndpoints()
        {
            group.MapGet("/", GetAllOutfits);
            group.MapPost("/", CreateOutfit);
            group.MapGet("/{id:Guid}", GetOutfit);
            group.MapPut("/{id:Guid}", EditOutfit);
            group.MapDelete("/{id:Guid}", DeleteOutfit);
        }
    }

    private static async Task<Results<Ok<OutfitGetAllResponse>, NotFound>> GetAllOutfits([FromQuery] Guid userId, IOutfitRepository outfitRepository)
    {
        var outfits = await outfitRepository.GetAllForUser(userId);
        var outfitDtos = outfits.Select(outfit => outfit.ToDto()).ToList();
        var response = new OutfitGetAllResponse(outfitDtos);
        return TypedResults.Ok(response);
    }

    private static async Task<Results<Ok<OutfitGetResponse>, NotFound>> GetOutfit(IOutfitRepository outfitRepository, Guid id)
    {
        var outfit = await outfitRepository.GetById(id);
        if (outfit == null)
            return TypedResults.NotFound();

        var outfitDto = outfit.ToDto();
        var response = new OutfitGetResponse(outfitDto);
        return TypedResults.Ok(response);
    }

    private static async Task<Results<Ok<OutfitMetadataResponse>, BadRequest<string>>> EditOutfit(
        HttpRequest request,
        HttpContext httpContext,
        ICmsAdapter cmsAdapter,
        IOutfitRepository outfitRepository,
        Guid id,
        [FromQuery] Guid userId
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
        var updatedOutfit = metadata.ToModel();

        if (multipartFormData.Any())
        {
            var fileInformation = await cmsAdapter.UploadFileAsync(multipartFormData, userId);
            updatedOutfit.AddImages(fileInformation);
        }

        updatedOutfit.UserId = userId;
        var savedOutfit = await outfitRepository.UpdateOutfit(updatedOutfit, id);

        return TypedResults.Ok(savedOutfit.ToDto());
    }

    private static async Task<Results<Ok<OutfitMetadataResponse>, BadRequest<string>>> CreateOutfit(
        HttpRequest request,
        HttpContext httpContext,
        ICmsAdapter cmsAdapter,
        IOutfitRepository outfitRepository,
        [FromQuery] Guid userId
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

        var fileInformation = await cmsAdapter.UploadFileAsync(multipartFormData, userId);

        var outfit = metadata.ToModel();

        outfit.UserId = userId;
        outfit.AddImages(fileInformation);

        var savedOutfit = await outfitRepository.AddOutfit(outfit);

        return TypedResults.Ok(savedOutfit.ToDto());
    }

    private static async Task<Results<NoContent, BadRequest<string>>> DeleteOutfit(
        IOutfitRepository outfitRepository,
        Guid id,
        [FromQuery] Guid userId)
    {
        var numberOfDeletedOutfits = await outfitRepository.DeleteOutfit(id, userId);
        if (numberOfDeletedOutfits == 0)
            return TypedResults.BadRequest("no outfits deleted");
        return TypedResults.NoContent();
    }

    private static async Task<OutfitPostRequestMetadata?> ParseInboundJsonMetadata(Stream bodySection, CancellationToken cancellationToken)
    {
        return await JsonSerializer.DeserializeAsync<OutfitPostRequestMetadata>(
            bodySection,
            JsonSerializerConfigurations.CaseInsensitive,
            cancellationToken);
    }
}