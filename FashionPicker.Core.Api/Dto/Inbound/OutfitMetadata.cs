namespace FashionPicker.Core.Api.Dto.Inbound;

public record OutfitMetadata(
    Guid? Id,
    string ImageUrl,
    List<OutfitTag> Tags,
    string? Season,
    List<string>? Colors
    );