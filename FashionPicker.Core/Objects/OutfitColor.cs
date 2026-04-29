namespace FashionPicker.Core.Objects;

public class OutfitColor
{
    public Guid Id { get; set; }
    public required string Value {get; set;}
    public List<Outfit> Outfits { get; set; } = [];
}