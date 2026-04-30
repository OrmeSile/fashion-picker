
using FashionPicker.Api.Dto.Inbound.ClothingRequest;
using FashionPicker.Api.Dto.Outbound.ClothingResponse;
using FashionPicker.Core.Objects;

namespace FashionPicker.Api.Converters;

internal static class ClothingConverter
{
    extension(Clothing model)
    {
        internal ClothingMetadataResponse ToDto()
        {
            return new ClothingMetadataResponse(
                Id: model.Id,
                ClothingType: model.Type.ToString(),
                Images: model.Images.Select(image => image.ToDto()).ToList()
            );
        }
    }

    extension(ClothingPostRequestMetadata dto)
    {
        internal Clothing ToModel()
        {
            return new Clothing
            {
                Type = dto.ClothingType.ToClothingTypeModel()
            };
        }
    }
}

file static class LocalConverters
{
    extension(string dto)
    {
        internal ClothingType ToClothingTypeModel() => dto.ToLowerInvariant() switch
        {
            "top" => ClothingType.Top,
            "bottom" => ClothingType.Bottom,
            "shoes" => ClothingType.Shoes,
            "jewelry" => ClothingType.Jewelry,
            "fullbody" => ClothingType.Fullbody,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, null)
        };
    }
}