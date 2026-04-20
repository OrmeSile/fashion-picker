namespace FashionPicker.Core.Infra.Models;

public class Clothing
{
    public Guid Id { get; init; }
    public ClothingType Type { get; init; }
    public List<string>? Images { get; init; }
}