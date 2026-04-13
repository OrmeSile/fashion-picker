namespace FashionPicker.Api.Dto.Inbound;

public record OutfitMetadata(
    string Name,
    string Description,
    string Season,
    List<string> Colors,
    List<OutfitTag> Tags
    );

public record OutfitTag(
    string Name,
    string Description
    );