using System.ComponentModel.DataAnnotations;

namespace FashionPicker.Core.Objects;

public class OutfitTag
{
    public Guid Id { get; init; }
    public required string Value { get; set; }
    public List<Outfit> Outfits { get; } = [];
}