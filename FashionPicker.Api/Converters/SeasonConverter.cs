using FashionPicker.Core.Objects;

namespace FashionPicker.Api.Converters;

internal static class SeasonConverter
{
    extension(string dto)
    {
        public Season ToSeasonModel()
        {
            return dto.ToLowerInvariant() switch
            {
                "spring" => new Season { Value = ESeason.Spring },
                "summer" => new Season { Value = ESeason.Summer },
                "autumn" => new Season { Value = ESeason.Autumn },
                "winter" => new Season { Value = ESeason.Winter },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    extension(Season model)
    {
        public string ToDto()
        {
            return model.Value switch
            {
                ESeason.Spring => "spring",
                ESeason.Summer => "summer",
                ESeason.Autumn => "autumn",
                ESeason.Winter => "winter",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}