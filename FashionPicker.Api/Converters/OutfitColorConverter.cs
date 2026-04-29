using FashionPicker.Core.Objects;

namespace FashionPicker.Api.Converters;

internal static class OutfitColorConverter
{
    extension(IEnumerable<string> dtos)
    {
        public IEnumerable<OutfitColor> ToColorModels()
        {
            return dtos.Select(ToColorModel);
        }
    }

    extension(string dto)
    {
        public OutfitColor ToColorModel()
        {
            return new OutfitColor
            {
                Value = dto
            };
        }
    }

    extension(IEnumerable<OutfitColor> models)
    {
        internal IEnumerable<string> ToDtos()
        {
            return models.Select(ToDto);
        }
    }

    extension(OutfitColor model)
    {
        internal string ToDto()
        {
            return model.Value;
        }
    }
}