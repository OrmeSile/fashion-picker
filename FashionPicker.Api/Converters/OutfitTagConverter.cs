namespace FashionPicker.Api.Converters;

public static class OutfitTagConverter
{
    extension(Dto.Inbound.OutfitTag dto)
    {
        public Infra.Models.OutfitTag toModel()
        {
            return new Infra.Models.OutfitTag
            {
                Name = dto.Name,
                Description = dto.Description,
                Id = dto.Id.GetValueOrDefault()
            };
        }
    }

    extension(Infra.Models.OutfitTag model)
    {
        public Dto.Inbound.OutfitTag toDto()
        {
            return new Dto.Inbound.OutfitTag
            (
                model.Id,
                model.Name,
                model.Description
            );
        }
    }
}