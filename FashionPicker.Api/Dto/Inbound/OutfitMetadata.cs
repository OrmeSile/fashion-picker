namespace FashionPicker.Api.Dto.Inbound;

public record OutfitMetadata(
    Guid? Id,
    string Name,
    string ImageUrl,
    List<OutfitTag> Tags,
    string? Description,
    string? Season,
    List<string>? Colors
    );