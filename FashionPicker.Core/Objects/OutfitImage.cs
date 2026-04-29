namespace FashionPicker.Core.Objects;

public class OutfitImage
{
    public Guid Id { get; set; }
    public required string Url { get; set; }
    public Outfit Outfit { get; set; } = null!;
}