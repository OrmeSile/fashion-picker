using System.Text.Json;
using FashionPicker.Core.Api.Converters;
using FashionPicker.Core.Api.Dto.Inbound;
using FashionPicker.Core.Api.Dto.Inbound.Outfit;
using FashionPicker.Core.Api.Dto.Outbound.Outfit;
using FashionPicker.Core.Infra.Adapters.LocalCMS;
using FashionPicker.Core.Infra.Models;
using FashionPicker.Core.Infra.Providers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using OutfitTag = FashionPicker.Core.Api.Dto.Inbound.OutfitTag;

namespace FashionPicker.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OutfitController(OutfitProvider outfitProvider, OutfitTagProvider outfitTagProvider, ICmsAdapter cmsAdapter) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OutfitGetResponse>> Get([FromBody] OutfitGetRequest request)
    {
        var outfit = await outfitProvider.GetById(request.Id);

        if (outfit == null)
            return NotFound();

        var metadata = new OutfitMetadata
        (
            outfit.Id,
            Colors: outfit.Colors,
            Season: outfit.Season,
            ImageUrl: outfit.ImageUrl,
            Tags: outfit.Tags.Select(tag => new OutfitTag(tag.Id, tag.Value)).ToList()
        );

        var response = new OutfitGetResponse(metadata);

        return Ok(response);
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    public async Task<Results<Ok<OutfitPostResponse>, BadRequest<string>>>CreateOutfit()
    {
        if (!Request.ContentType?.StartsWith("multipart/form-data") ?? true)
            return TypedResults.BadRequest("the request does not contain multipart/form-data");

        var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
        if (string.IsNullOrWhiteSpace(boundary))
            return TypedResults.BadRequest("Missing boundary header");

        var reader = new MultipartReader(boundary, Request.Body);

        var cancellationToken = HttpContext.RequestAborted;

        var multipartFormData = new MultipartFormDataContent();

        OutfitPostRequestMetadataBody? metadata = null;
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
                    var metadataStream = new MemoryStream();
                    await section.Body.CopyToAsync(metadataStream, cancellationToken);
                    metadataStream.Position = 0;

                    metadata = await ParseJsonMetadata(metadataStream, cancellationToken) ?? throw new InvalidOperationException("missing metadata");
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
            if(metadata == null)
                throw new InvalidOperationException("missing metadata");
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }

        var fileInformation = await cmsAdapter.UploadFileAsync(multipartFormData);
        var dbTags = await outfitTagProvider.GetOrCreateOutfitTags(metadata.Tags);

        var outfit = new Outfit
        {
            CreationDate = DateTime.UtcNow,
            ImageUrl = fileInformation.PathOriginal,
            Tags = dbTags ?? [],
            Colors = metadata.Colors,
            Season = "winter",
        };

        var savedOutfits = await outfitProvider.AddRange([outfit]);

        return TypedResults.Ok(new OutfitPostResponse(savedOutfits.Select(so => so.ToDto()).ToList()));
    }



    private async Task<OutfitPostRequestMetadataBody?> ParseJsonMetadata(Stream bodySection, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await JsonSerializer.DeserializeAsync<OutfitPostRequestMetadataBody>(
            bodySection,
            options,
            cancellationToken: cancellationToken);
    }
}