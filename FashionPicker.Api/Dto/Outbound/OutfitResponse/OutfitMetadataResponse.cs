using FashionPicker.Api.Converters;
using FashionPicker.Api.Dto.Outbound.ClothingResponse;

namespace FashionPicker.Api.Dto.Outbound.OutfitResponse;

public record OutfitMetadataResponse(
    List<string> Tags,
    List<string> Seasons,
    List<string> Colors,
    Guid Id,
    string Mood,
    bool Sport,
    List<ImageDto> Images,
    List<ClothingMetadataResponse> Clothing
);