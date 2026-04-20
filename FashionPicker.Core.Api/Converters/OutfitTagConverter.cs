namespace FashionPicker.Core.Api.Converters;

public static class OutfitTagConverter
{
    extension(Dto.Inbound.OutfitTag dto)
    {
        public Core.Infra.Models.OutfitTag toModel()
        {
            return new Core.Infra.Models.OutfitTag
            {
                Value = dto.Value,
                Id = dto.Id.GetValueOrDefault()
            };
        }
    }

    extension(Core.Infra.Models.OutfitTag model)
    {
        public Dto.Inbound.OutfitTag toDto()
        {
            return new Dto.Inbound.OutfitTag
            (
                model.Id,
                model.Value
            );
        }
    }
}