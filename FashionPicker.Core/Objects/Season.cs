namespace FashionPicker.Core.Objects;

public class Season
{
    public Guid Id { get; set; }
    public ESeason Value { get; init; }
    public List<Outfit> Outfits { get; init; }
}