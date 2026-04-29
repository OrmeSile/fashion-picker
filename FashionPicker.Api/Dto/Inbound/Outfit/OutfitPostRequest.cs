namespace FashionPicker.Api.Dto.Inbound.Outfit;

public record OutfitPostRequestMetadataBody(
    Guid? Id,
    Seasons Seasons,
    List<string> Colors,
    List<string> Tags,
    string Mood,
    bool Sport);

public record Seasons(
    bool Spring,
    bool Summer,
    bool Autumn,
    bool Winter);