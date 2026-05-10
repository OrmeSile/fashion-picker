using FashionPicker.Core.Objects;
using FashionPicker.Core.Repositories;
using FashionPicker.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infrastructure.Repositories;

public class ClothingRepository(OutfitDbContext outfitDbContext) : IClothingRepository
{
    public async Task<List<Clothing>> GetAll()
    {
        return await outfitDbContext.Clothing
            .Include(c => c.Images)
            .ToListAsync();
    }

    public async Task<List<Clothing>> GetAllWithOutfits(string clothingId)
    {
        return await outfitDbContext.Clothing.Include(c => c.Outfits).ToListAsync();
    }

    public async Task<Clothing> AddClothing(Clothing clothing)
    {
        await outfitDbContext.Clothing.AddAsync(clothing);
        await outfitDbContext.SaveChangesAsync();
        return clothing;
    }
}