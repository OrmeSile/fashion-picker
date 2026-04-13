namespace FashionPicker.Infra.Models;

public class Outfit
{
    public Guid Id { get; set; }
    public string OutfitName { get; set; }
    public string OutfitDescription { get; set; }
    public string OutfitImage { get; set; }
    public DateTime OutfitCreationDate { get; set; }
    public List<OutfitTag> OutfitTags { get; set; }
    public string OutfitSeason { get; set; }
}