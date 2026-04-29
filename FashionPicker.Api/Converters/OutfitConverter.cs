using FashionPicker.Api.Dto.Inbound;
using FashionPicker.Core.Objects;

namespace FashionPicker.Api.Converters;

internal static class OutfitConverter
{
    extension(OutfitMetadata dto)
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
                Sport =  dto.Sport
            };
        }
    }

    extension(Outfit model)
    {
        internal OutfitMetadata ToDto()
        {
            return new OutfitMetadata(
                Tags: model.Tags.Select(outfitTag => outfitTag.ToDto()).ToList(),
                Seasons: model.Seasons.Select(s => s.ToDto()).ToList(),
                Colors: model.Colors.Select(c => c.ToDto()).ToList(),
                Id: null,
                Mood: model.Mood.ToDto(),
                Sport: model.Sport
            );
        }

        internal OutfitMetadata ToDtoWithId()
        {
            return new OutfitMetadata(
                Tags: model.Tags.Select(outfitTag => outfitTag.ToDto()).ToList(),
                Seasons: model.Seasons.Select(s => s.ToDto()).ToList(),
                Colors: model.Colors.Select(c => c.ToDto()).ToList(),
                Id: model.Id,
                Mood: model.Mood.ToDto(),
                Sport: model.Sport
            );
        }
    }
}

file static class MoodConverter
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
        internal string ToDto() => model switch
        {
            Mood.Low => "low",
            Mood.Medium => "medium",
            Mood.High => "high",
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }
}