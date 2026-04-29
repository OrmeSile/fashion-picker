using FashionPicker.Core.Objects;
using Microsoft.EntityFrameworkCore;

namespace FashionPicker.Infrastructure.DbContexts;

public class OutfitDbContext(DbContextOptions<OutfitDbContext> options): DbContext(options)
{
    public DbSet<Outfit> Outfits { get; set; }
    public DbSet<OutfitTag> OutfitTags { get; set; }
    public DbSet<Clothing> Clothing { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutfitDbContext).Assembly);
    }
}