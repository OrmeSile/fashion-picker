using FashionPicker.Core.Api.Converters;
using FashionPicker.Core.Api.Dto.Inbound;
using FashionPicker.Core.Api.Dto.Inbound.Outfit;
using FashionPicker.Core.Api.Dto.Outbound.Outfit;
using FashionPicker.Core.Infra.Models;
using FashionPicker.Core.Infra.Providers;
using Microsoft.AspNetCore.Mvc;
using OutfitTag = FashionPicker.Core.Api.Dto.Inbound.OutfitTag;

namespace FashionPicker.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OutfitController(OutfitProvider outfitProvider) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OutfitGetResponse>> Get([FromBody]OutfitGetRequest request)
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
    public async Task<ActionResult<OutfitPostResponse>> Post([FromBody]OutfitPostRequest request)
    {
        var outfits = request.Outfits.Select<OutfitMetadata, Outfit>(outfit => outfit.toModel()).ToList();
        var addedOutfits = await outfitProvider.AddRange(outfits);
        var outfitDtos = addedOutfits.Select<Outfit, OutfitMetadata>(outfit => outfit.toDto()).ToList();
        var response = new OutfitPostResponse(outfitDtos);
        return Ok(response);
    }
}