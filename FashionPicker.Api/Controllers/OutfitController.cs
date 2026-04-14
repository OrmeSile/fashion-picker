using FashionPicker.Api.Dto.Inbound;
using FashionPicker.Api.Dto.Inbound.Outfit;
using FashionPicker.Infra.Models;
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
            Name: outfit.Name,
            Description: outfit.Description,
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
        var outfits = request.Outfits.Select(outfitMetadata =>
        {
            return new Outfit
            {
                Name = outfitMetadata.Name,
                Colors = outfitMetadata.Colors,
                Season = outfitMetadata.Season,
                Description = outfitMetadata.Description,
                CreationDate = DateTime.UtcNow,
                ImageUrl = outfitMetadata.ImageUrl,
                Tags = outfitMetadata.Tags.Select(outfitTag =>
                        new Infra.Models.OutfitTag
                        {
                            Name = outfitTag.Name,
                            Description = outfitTag.Description,
                            Id = outfitTag.Id.GetValueOrDefault()
                        })
                    .ToList()
            };
        }).ToList();
        var addedOutfits = await outfitProvider.AddRange(outfits);
        var outfitDtos = addedOutfits.Select(outfit =>
        {
            var outfitMetadata = new OutfitMetadata(
                Id: outfit.Id,
                Colors: outfit.Colors,
                Name: outfit.Name,
                ImageUrl: outfit.ImageUrl,
                Description: outfit.Description,
                Season: outfit.Season,
                Tags: outfit.Tags.Select(outfitTag =>
                    new OutfitTag
                        (
                            outfitTag.Id,
                            outfitTag.Name,
                            outfitTag.Description
                        )
                ).ToList()
            );
            return outfitMetadata;
        }).ToList();
        var response = new OutfitPostResponse(outfitDtos);
        return Ok(response);
    }
}