using FashionPicker.Core.Objects;
using FashionPicker.Core.Repositories;
using FashionPicker.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infrastructure.Repositories;

public class OutfitRepository(OutfitDbContext outfitContext) : IOutfitRepository
{
    public async Task<List<Outfit>> GetAll()
    {
        return await outfitContext.Outfits.ToListAsync();
    }

    public async Task<Outfit?> GetById(Guid id)
    {
        return await outfitContext.Outfits.FindAsync(id);
    }

    public async Task<Outfit> AddOutfit(Outfit outfit)
    {
        var dbTags = outfitContext.OutfitTags
            .Where(dbTag =>
                outfit.Tags
                    .Select(t => t.Value)
                    .Contains(dbTag.Value)
            ).ToList();

        var combinedTags = outfit.Tags
            .Select(tag =>
                dbTags.FirstOrDefault(dbTag => dbTag.Value == tag.Value) ?? tag
            ).ToList();

        outfit.Tags = combinedTags;

        await outfitContext.Outfits.AddRangeAsync(outfit);
        await outfitContext.SaveChangesAsync();
        return outfit;
    }
}