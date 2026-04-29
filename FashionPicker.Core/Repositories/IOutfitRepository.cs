using FashionPicker.Core.Objects;

namespace FashionPicker.Core.Repositories;

public interface IOutfitRepository
{
    Task<List<Outfit>> GetAll();
    Task<Outfit?> GetById(Guid id);
    Task<Outfit> AddOutfit(Outfit outfit);
}