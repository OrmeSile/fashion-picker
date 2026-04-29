namespace FashionPicker.Core.Objects;

public class ClothingImage
{
    public Guid Id { get; init; }
    public required string Url { get; init; }
    public required Clothing Clothing { get; init; }
}