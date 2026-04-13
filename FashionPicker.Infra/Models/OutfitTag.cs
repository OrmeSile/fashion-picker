namespace FashionPicker.Infra.Models;

public class OutfitTag
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}