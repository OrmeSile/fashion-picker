namespace FashionPicker.Core.Objects;

public class Clothing
{
    public Guid Id { get; init; }
    public ClothingType Type { get; init; }
    public List<ClothingImage> Images { get; init; } = [];
}