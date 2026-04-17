using FashionPicker.Api.Converters;
using FashionPicker.Api.Dto.Inbound;
using FashionPicker.Api.Dto.Inbound.Outfit;
using FashionPicker.Infra.Providers;
using Microsoft.AspNetCore.Mvc;
using OutfitTag = FashionPicker.Api.Dto.Inbound.OutfitTag;

namespace FashionPicker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OutfitController(OutfitProvider outfitProvider, OutfitTagProvider outfitTagProvider) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OutfitGetResponse>> Get(OutfitGetRequest request)
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
            Tags: outfit.Tags.Select(tag => new OutfitTag(tag.Id, tag.Name, tag.Description)).ToList()
        );

        var response = new OutfitGetResponse(metadata);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OutfitPostResponse>> Post(OutfitPostRequest request)
    {
        var outfits = request.Outfits.Select(outfit => outfit.toModel()).ToList();
        var addedOutfits = await outfitProvider.AddRange(outfits);
        var outfitDtos = addedOutfits.Select(outfit => outfit.toDto()).ToList();
        var response = new OutfitPostResponse(outfitDtos);
        return Ok(response);
    }
}