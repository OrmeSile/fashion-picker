using FashionPicker.Core.Objects;
using FashionPicker.Core.Repositories;
using FashionPicker.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infrastructure.Repositories;

public class ClothingRepository(OutfitDbContext outfitDbContext) : IClothingRepository
{
    public async Task<List<Clothing>> GetAll()
    {
        return await outfitDbContext.Clothing.ToListAsync();
    }

    public async Task<List<Clothing>> GetAllWithOutfits(string clothingId)
    {
        return await outfitDbContext.Clothing.Include(c => c.Outfits).ToListAsync();
    }

    public Task<Clothing> AddClothing(Clothing clothing)
    {
        throw new NotImplementedException();
    }
}