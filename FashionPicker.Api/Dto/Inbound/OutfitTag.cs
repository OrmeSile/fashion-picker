namespace FashionPicker.Api.Dto.Inbound;

public record OutfitTag(
    Guid? Id,
    string Name,
    string? Description
);