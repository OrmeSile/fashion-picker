using FashionPicker.Core.Infra.DbContexts;
using FashionPicker.Core.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Core.Infra.Providers;

public class OutfitTagProvider(OutfitDbContext context)
{
    public async Task<List<OutfitTag>> GetAll()
    {
        return await context.OutfitTags.ToListAsync();
    }

    public async Task<OutfitTag?> GetById(int id)
    {
        return await context.OutfitTags.FindAsync(id);
    }

    public async Task<OutfitTag?> GetByValue(string value)
    {
        return await context.OutfitTags.FirstOrDefaultAsync(t => t.Value == value);
    }

    public async Task<List<OutfitTag>> AddRange(List<OutfitTag> outfitTags)
    {
        await context.OutfitTags.AddRangeAsync(outfitTags);
        await context.SaveChangesAsync();
        return outfitTags;
    }
}