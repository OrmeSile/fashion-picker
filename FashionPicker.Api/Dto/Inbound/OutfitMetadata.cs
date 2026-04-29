namespace FashionPicker.Api.Dto.Inbound;

public record OutfitMetadata(
    List<string> Tags,
    List<string> Seasons,
    List<string> Colors,
    Guid? Id,
    string Mood,
    bool Sport
);

public record ColorDto(string Value);