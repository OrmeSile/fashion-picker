using FashionPicker.Core.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Core.Infra.DbContexts;

public class OutfitDbContext(DbContextOptions<OutfitDbContext> options): DbContext(options)
{
    public DbSet<Outfit> Outfits { get; set; }
    public DbSet<OutfitTag> OutfitTags { get; set; }
    public DbSet<Clothing> Clothing { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Clothing>()
            .Property(c => c.Type)
            .HasConversion<string>();
    }
}