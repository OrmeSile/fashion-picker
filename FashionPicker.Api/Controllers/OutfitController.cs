using System.Text.Json;
using FashionPicker.Api.Converters;
using FashionPicker.Api.Dto.Inbound;
using FashionPicker.Api.Dto.Inbound.Outfit;
using FashionPicker.Api.Dto.Outbound.Outfit;
using FashionPicker.Core.Adapters;
using FashionPicker.Core.Objects;
using FashionPicker.Infrastructure.Adapters.LocalCMS;
using FashionPicker.Infrastructure.Providers;
using FashionPicker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FashionPicker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OutfitController(OutfitRepository outfitRepository, OutfitTagProvider outfitTagProvider, ICmsAdapter cmsAdapter) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OutfitGetResponse>> Get([FromBody] OutfitGetRequest request)
    {
        var outfit = await outfitRepository.GetById(request.Id);

        if (outfit == null)
            return NotFound();

        var response = new OutfitGetResponse(outfit.ToDto());

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

        OutfitMetadata? metadata = null;
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
        var outfit = metadata.ToModel();
        outfit.AddImages(fileInformation);
        var savedOutfits = await outfitRepository.AddRange([outfit]);

        return TypedResults.Ok(new OutfitPostResponse(savedOutfits.Select(so => so.ToDtoWithId()).ToList()));
    }



    private async Task<OutfitMetadata?> ParseJsonMetadata(Stream bodySection, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await JsonSerializer.DeserializeAsync<OutfitMetadata>(
            bodySection,
            options,
            cancellationToken: cancellationToken);
    }
}