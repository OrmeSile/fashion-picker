using FashionPicker.Infra.DbContexts;
using FashionPicker.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infra.Providers;

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

    public async Task<OutfitTag?> GetByName(string name)
    {
        return await context.OutfitTags.FirstOrDefaultAsync(t => t.Name == name);
    }

    public async Task<List<OutfitTag>> AddRange(List<OutfitTag> outfitTags)
    {
        await context.OutfitTags.AddRangeAsync(outfitTags);
        await context.SaveChangesAsync();
        return outfitTags;
    }
}