using FashionPicker.Api.Converters;

namespace FashionPicker.Api.Dto.Outbound.ClothingResponse;

public record ClothingMetadataResponse(
    Guid Id,
    string ClothingType,
    List<ImageDto> Images
    );