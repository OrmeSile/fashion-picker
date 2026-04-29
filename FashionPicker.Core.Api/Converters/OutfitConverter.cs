using FashionPicker.Core.Api.Dto.Inbound;

namespace FashionPicker.Core.Api.Converters;

public static class OutfitConverter
{
    extension(OutfitMetadata dto)
    {
        public Core.Infra.Models.Outfit ToModel()
        {
            return new Core.Infra.Models.Outfit
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
        public OutfitMetadata ToDto()
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
                        outfitTag.Value
                    )
                ).ToList()
            );
        }
    }
}