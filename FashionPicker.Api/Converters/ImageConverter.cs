using FashionPicker.Core.Objects;

namespace FashionPicker.Api.Converters;

public static class ImageConverter
{
    extension(OutfitImage model)
    {
        internal ImageDto ToDto()
        {
            return new ImageDto(
                model.SmallSizeUrl,
                model.MediumSizeUrl,
                model.BigSizeUrl,
                model.OriginalSizeUrl,
                model.MimeType
            );
        }
    }

    extension(ClothingImage model)
    {
        internal ImageDto ToDto()
        {
            return new ImageDto(
                model.SmallSizeUrl,
                model.MediumSizeUrl,
                model.BigSizeUrl,
                model.OriginalSizeUrl,
                model.MimeType
            );
        }
    }
}