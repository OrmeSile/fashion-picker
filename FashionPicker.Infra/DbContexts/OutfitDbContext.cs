using FashionPicker.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infra.DbContexts;

public class OutfitDbContext(DbContextOptions<OutfitDbContext> options): DbContext(options)
{
    public DbSet<Outfit> Outfits { get; set; }
    public DbSet<OutfitTag> OutfitTags { get; set; }
}