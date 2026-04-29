using FashionPicker.Api.Converters;
using FashionPicker.Api.Dto.Inbound;

namespace FashionPicker.Api.Dto.Outbound.Outfit;

public record OutfitMetadataResponse(
    List<string> Tags,
    List<string> Seasons,
    List<string> Colors,
    Guid Id,
    string Mood,
    bool Sport,
    List<ImageDto> Images
);