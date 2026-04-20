using FashionPicker.Core.Infra.DbContexts;
using FashionPicker.Core.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Core.Infra.Providers;

public class ClothingProvider(OutfitDbContext context)
{
    public async Task<List<Clothing>> GetAll()
    {
        return await context.Clothing.ToListAsync();
    }

    public async Task<Clothing?> GetById(Guid id)
    {
        return await context.Clothing.FindAsync(id);
    }

    public async Task<List<Clothing>> AddRange(List<Clothing> clothing)
    {
        await context.Clothing.AddRangeAsync(clothing);
        await context.SaveChangesAsync();
        return clothing;
    }
}