namespace FashionPicker.Core.Objects;

public class Outfit
{
    public Guid Id { get; set; }
    public required List<OutfitTag> Tags { get; set; }
    public required DateTime CreationDate { get; set; }
    public string? Description { get; set; }
    public List<OutfitImage> Images { get; set; } = [];
    public List<Season> Seasons { get; set; } = [];
    public List<OutfitColor> Colors { get; set; } = [];
    public Mood Mood { get; set; }
    public bool Sport { get; set; }

    public void AddImages(RepositoryFileInformation fileInformation)
    {
        Images.Add(new OutfitImage
        {
            Url = fileInformation.PathOriginal
        });
    }
}