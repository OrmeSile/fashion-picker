namespace FashionPicker.Core.Objects;

public class Clothing
{
    public Guid Id { get; init; }
    public ClothingType Type { get; init; }
    public List<ClothingImage> Images { get; init; } = [];
    public List<Outfit> Outfits { get; init; } = [];

    public void AddImages(RepositoryFileInformation fileInformation)
    {
        var clothingImage = new ClothingImage
        {
            MimeType = fileInformation.MimeType,
            SmallSizeUrl = fileInformation.PathSmall,
            MediumSizeUrl = fileInformation.PathMedium,
            BigSizeUrl = fileInformation.PathBig,
            OriginalSizeUrl = fileInformation.PathOriginal
        };

        Images.Add(clothingImage);
    }
}