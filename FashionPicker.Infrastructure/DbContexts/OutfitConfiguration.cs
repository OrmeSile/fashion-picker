using FashionPicker.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionPicker.Infrastructure.DbContexts;

internal class OutfitConfiguration : IEntityTypeConfiguration<Outfit>
{
    public void Configure(EntityTypeBuilder<Outfit> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.CreationDate).IsRequired();
        builder.Property(x => x.Mood).HasConversion<string>().IsRequired();

        builder.HasMany(x => x.Images).WithOne(x => x.Outfit).HasForeignKey("OutfitId");
        builder.HasMany(x => x.Colors).WithMany(x => x.Outfits);
        builder.HasMany(x => x.Seasons).WithMany(x => x.Outfits);
        builder.HasMany(x => x.Tags).WithMany(x => x.Outfits);
    }
}