using FashionPicker.Core.Objects;

namespace FashionPicker.Core.Repositories;

public interface IClothingRepository
{
    Task<List<Clothing>> GetAll();
    Task<List<Clothing>> GetAllWithOutfits(string clothingId);
    Task<Clothing> AddClothing(Clothing clothing);
}