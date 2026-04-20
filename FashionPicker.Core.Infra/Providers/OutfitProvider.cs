using FashionPicker.Core.Infra.DbContexts;
using FashionPicker.Core.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Core.Infra.Providers;

public class OutfitProvider(OutfitDbContext outfitContext)
{
    public async Task<List<Outfit>> GetAll()
    {
        return await outfitContext.Outfits.ToListAsync();
    }

    public async Task<Outfit?> GetById(Guid id)
    {
        return await outfitContext.Outfits.FindAsync(id);
    }

    public async Task<List<Outfit>> AddRange(List<Outfit> outfits)
    {
        await outfitContext.Outfits.AddRangeAsync(outfits);
        await outfitContext.SaveChangesAsync();
        return outfits;
    }
}