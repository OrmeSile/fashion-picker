namespace FashionPicker.Core.Objects;

public class Outfit
{
    public Guid Id { get; init; }
    public required List<OutfitTag> Tags { get; init; }
    public required DateTime CreationDate { get; init; }
    public string? Description { get; init; }
    public List<OutfitImage> Images { get; init; } = [];
    public List<Season> Seasons { get; init; } = [];
    public List<OutfitColor> Colors { get; init; } = [];
    public Mood Mood { get; init; }
    public bool Sport { get; init; }

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

    public void AddTags(IEnumerable<OutfitTag> combinedTags)
    {
        Tags.AddRange(combinedTags);
    }
}