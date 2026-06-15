using FashionPicker.Core.Objects;
using FashionPicker.Core.Repositories;
using FashionPicker.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infrastructure.Repositories;

public class ClothingRepository(OutfitDbContext outfitDbContext) : IClothingRepository
{
    public async Task<List<Clothing>> GetAllForUser(Guid userId)
    {
        return await outfitDbContext.Clothing
            .Include(c => c.Images)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Clothing>> GetAllForUserWithOutfits(Guid userId, string clothingId)
    {
        return await outfitDbContext.Clothing
            .Include(c => c.Outfits)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Clothing> AddClothing(Clothing clothing)
    {
        await outfitDbContext.Clothing.AddAsync(clothing);
        await outfitDbContext.SaveChangesAsync();
        return clothing;
    }
}