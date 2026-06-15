using FashionPicker.Core.Objects;

namespace FashionPicker.Core.Repositories;

public interface IClothingRepository
{
    Task<List<Clothing>> GetAllForUser(Guid userId);
    Task<List<Clothing>> GetAllForUserWithOutfits(Guid userId, string clothingId);
    Task<Clothing> AddClothing(Clothing clothing);
}