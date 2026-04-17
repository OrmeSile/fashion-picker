using FashionPicker.Api.Dto.Inbound;

namespace FashionPicker.Api.Converters;

public static class OutfitConverter
{
    extension(OutfitMetadata dto)
    {
        public Infra.Models.Outfit toModel()
        {
            return new Infra.Models.Outfit
            {
                Colors = dto.Colors,
                Season = dto.Season,
                CreationDate = DateTime.UtcNow,
                ImageUrl = dto.ImageUrl,
                Tags = dto.Tags.Select(tag => tag.toModel())
                    .ToList()
            };
        }
    }

    extension(Infra.Models.Outfit model)
    {
        public OutfitMetadata toDto()
        {
            return new OutfitMetadata(
                model.Id,
                Colors: model.Colors,
                ImageUrl: model.ImageUrl,
                Season: model.Season,
                Tags: model.Tags.Select(outfitTag =>
                    new OutfitTag
                    (
                        outfitTag.Id,
                        outfitTag.Name,
                        outfitTag.Description
                    )
                ).ToList()
            );
        }
    }
}