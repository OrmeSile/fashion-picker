using FashionPicker.Core.Objects;

namespace FashionPicker.Core.Repositories;

public interface IOutfitRepository
{
    Task<List<Outfit>> GetAll();
    Task<List<Outfit>> GetAllForUser(Guid userId);
    Task<Outfit?> GetById(Guid id);
    Task<Outfit> AddOutfit(Outfit outfit);
}