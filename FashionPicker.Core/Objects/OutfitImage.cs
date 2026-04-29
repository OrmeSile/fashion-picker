namespace FashionPicker.Core.Objects;

public class OutfitImage
{
    public Guid Id { get; set; }
    public required string SmallSizeUrl { get; init; }
    public string? MediumSizeUrl { get; init; }
    public string? BigSizeUrl { get; init; }
    public required string OriginalSizeUrl { get; init; }
    public required string MimeType { get; init; }
    public Outfit Outfit { get; set; } = null!;
}