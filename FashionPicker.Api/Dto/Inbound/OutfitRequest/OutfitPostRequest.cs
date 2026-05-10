namespace FashionPicker.Api.Dto.Inbound.OutfitRequest;

public record OutfitPostRequestMetadata(
    List<string> Tags,
    List<string> Seasons,
    List<string> Colors,
    Guid? Id,
    string Mood,
    bool Sport,
    List<Guid> ClothingIds
);