namespace FashionPicker.Core.Objects;

public class Outfit
{
    public Guid Id { get; init; }
    public required DateTime CreationDate { get; init; }
    public List<OutfitImage> Images { get; init; } = [];
    public Guid UserId { get; set; }
    public required List<OutfitTag> Tags { get; set; } = [];
    public string? Description { get; set; }
    public List<Season> Seasons { get; set; } = [];
    public List<OutfitColor> Colors { get; set; } = [];
    public Mood Mood { get; set; }
    public bool Sport { get; set; }
    public List<Clothing> Clothing { get; set; } = [];

    public void AddImages(RepositoryFileInformation fileInformation)
    {
        var outfitImage = new OutfitImage
        {
            MimeType = fileInformation.MimeType,
            SmallSizeUrl = fileInformation.PathSmall,
            MediumSizeUrl = fileInformation.PathMedium,
            BigSizeUrl = fileInformation.PathBig,
            OriginalSizeUrl = fileInformation.PathOriginal
        };

        Images.Add(outfitImage);
    }

    public void AddTags(List<OutfitTag> combinedTags)
    {
        Tags = Tags.Union(combinedTags, new TagEqualityComparer()).ToList();
    }

}

file class TagEqualityComparer : EqualityComparer<OutfitTag>
{
    public override bool Equals(OutfitTag? t1, OutfitTag? t2)
    {
        if (t1 is null && t2 is null)
            return true;
        if(t1 is null || t2 is null)
            return false;

        return (t1.Value == t2.Value && t1.Id == t2.Id);
    }

    public override int GetHashCode(OutfitTag tag)
    {
        return tag.GetHashCode();
    }
}