using FashionPicker.Core.Objects;

namespace FashionPicker.Api.Converters;

internal static class OutfitTagConverter
{
    extension(string dto)
    {
        public OutfitTag ToOutfitTagModel() => new OutfitTag{Value = dto};
    }

    extension(OutfitTag model)
    {
        public string ToDto() => model.Value;
    }
}