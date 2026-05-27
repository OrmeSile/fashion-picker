using FashionPicker.Api.Dto.Inbound.OutfitRequest;
using FashionPicker.Api.Dto.Outbound.OutfitResponse;
using FashionPicker.Core.Objects;

namespace FashionPicker.Api.Converters;

internal static class OutfitConverter
{
    extension(OutfitPostRequestMetadata dto)
    {
        internal Outfit ToModel()
        {
            return new Outfit
            {
                Colors = dto.Colors.Select(color => color.ToColorModel()).ToList(),
                Seasons = dto.Seasons.Select(s => s.ToSeasonModel()).ToList(),
                CreationDate = DateTime.UtcNow,
                Tags = dto.Tags.Select(tag => tag.ToOutfitTagModel()).ToList(),
                Mood = dto.Mood.ToMoodModel(),
                Sport = dto.Sport,
                Clothing = dto.Clothing.Select(id => new Clothing{ Id = id }).ToList(),
            };
        }
    }

    extension(Outfit model)
    {
        internal OutfitMetadataResponse ToDto()
        {
            return new OutfitMetadataResponse(
                model.Tags.Select(outfitTag => outfitTag.ToDto()).ToList(),
                model.Seasons.Select(s => s.ToDto()).ToList(),
                model.Colors.Select(c => c.ToDto()).ToList(),
                model.Id,
                model.Mood.ToDto(),
                model.Sport,
                model.Images.Select(modelImage => modelImage.ToDto()).ToList(),
                model.Clothing.Select(clothing => clothing.ToDto()).ToList()
            );
        }
    }
}

file static class LocalConverters
{
    extension(string dto)
    {
        internal Mood ToMoodModel()
        {
            return dto switch
            {
                "low" => Mood.Low,
                "medium" => Mood.Medium,
                "high" => Mood.High,
                _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, null)
            };
        }
    }

    extension(Mood model)
    {
        internal string ToDto()
        {
            return model switch
            {
                Mood.Low => "low",
                Mood.Medium => "medium",
                Mood.High => "high",
                _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
            };
        }
    }
}