using FashionPicker.Core.Objects;
using FashionPicker.Core.Repositories;
using FashionPicker.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infrastructure.Repositories;

public class OutfitRepository(OutfitDbContext outfitContext) : IOutfitRepository
{
    public async Task<List<Outfit>> GetAll()
    {
        return await outfitContext.Outfits
            .Include(x => x.Tags)
            .Include(x => x.Images)
            .Include(x => x.Colors)
            .Include(x => x.Seasons)
            .Include(x => x.Clothing)
            .ToListAsync();
    }

    public async Task<List<Outfit>> GetAllForUser(Guid userId)
    {
        return await outfitContext.Outfits
            .Where(o => o.UserId == userId)
            .Include(x => x.Tags)
            .Include(x => x.Images)
            .Include(x => x.Colors)
            .Include(x => x.Seasons)
            .Include(x => x.Clothing)
            .ToListAsync();
    }

    public async Task<Outfit?> GetById(Guid id)
    {
        return await outfitContext.Outfits
            .Include(x => x.Tags)
            .Include(x => x.Images)
            .Include(x => x.Colors)
            .Include(x => x.Seasons)
            .Include(x => x.Clothing)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Outfit> AddOutfit(Outfit outfit)
    {
        var combinedTags = PrepareTags(outfit.Tags);
        outfit.AddTags(combinedTags);

        var savedClothing = await outfitContext.Clothing.Where(c => outfit.Clothing.Select(x => x.Id).Contains(c.Id)).ToListAsync();
        outfit.Clothing = savedClothing;
        await outfitContext.Outfits.AddRangeAsync(outfit);
        await outfitContext.SaveChangesAsync();
        return outfit;
    }

    public async Task<Outfit> UpdateOutfit(Outfit outfit, Guid id)
    {
        var savedOutfit = await GetById(id);
        if(savedOutfit == null)
            throw new Exception("Outfit not found");


        savedOutfit.Tags = savedOutfit.Tags
            .TakeWhile(t => outfit.Tags.Any(x => x.Value == t.Value))
            .ToList();

        foreach (var tag in outfit.Tags.Where(tag => savedOutfit.Tags.All(t => t.Value != tag.Value)))
        {
            var dbTag = await outfitContext.OutfitTags.FirstOrDefaultAsync(x => x.Value == tag.Value);
            savedOutfit.Tags.Add(dbTag ?? tag);
        }

        var savedClothing = await outfitContext.Clothing.Where(c => outfit.Clothing.Select(x => x.Id).Contains(c.Id)).ToListAsync();
        outfit.Clothing = savedClothing;

        savedOutfit.Colors = outfit.Colors;
        savedOutfit.Mood = outfit.Mood;
        savedOutfit.Sport = outfit.Sport;
        savedOutfit.Description = outfit.Description;
        savedOutfit.Seasons = outfit.Seasons;
        outfitContext.Outfits.Update(savedOutfit);
        await outfitContext.SaveChangesAsync();
        return savedOutfit;
    }

    public async Task<int> DeleteOutfit(Guid id, Guid userId)
    {
        return await outfitContext.Outfits
            .Where(o => o.Id == id && o.UserId ==  userId)
            .ExecuteDeleteAsync();
    }

    private List<OutfitTag> PrepareTags(List<OutfitTag> outfitTags)
    {
        var dbTags = outfitContext.OutfitTags
            .Where(dbTag =>
                outfitTags
                    .Select(t => t.Value)
                    .Contains(dbTag.Value)
            ).ToList();

        var combinedTags = outfitTags
            .Select(tag =>
                dbTags.FirstOrDefault(dbTag => dbTag.Value == tag.Value) ?? tag
            ).ToList();

        return combinedTags;
    }
}