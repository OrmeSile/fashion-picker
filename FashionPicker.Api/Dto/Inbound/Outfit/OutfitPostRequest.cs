namespace FashionPicker.Api.Dto.Inbound.Outfit;

public record OutfitPostRequestMetadata(
    List<string> Tags,
    List<string> Seasons,
    List<string> Colors,
    Guid? Id,
    string Mood,
    bool Sport
);