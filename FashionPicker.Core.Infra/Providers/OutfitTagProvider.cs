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

    public async Task<List<OutfitTag>> GetRangeByValues(List<string> values)
    {
        return await context.OutfitTags.Where(t => values.Contains(t.Value)).ToListAsync();
    }

    public async Task<List<OutfitTag>> AddRange(List<OutfitTag> outfitTags)
    {
        await context.OutfitTags.AddRangeAsync(outfitTags);
        return outfitTags;
    }

    public async Task<List<OutfitTag>?> GetOrCreateOutfitTags(List<string>? stringTags)
    {
        if (stringTags == null)
            return null;
        var tags = await GetRangeByValues(stringTags);

        var tagDict = new Dictionary<string, Core.Infra.Models.OutfitTag>();
        foreach (var tag in stringTags)
        {
            var dbTag = tags.FirstOrDefault(t => t.Value == tag);
            if (dbTag == null)
                tagDict[tag] = new OutfitTag(){Value = tag};
        }

        var dbTags = await AddRange(tagDict.Values.ToList());
        await context.SaveChangesAsync();
        return [..dbTags, ..tags];
    }
}