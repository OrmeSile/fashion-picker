using FashionPicker.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionPicker.Infrastructure.DbContexts.Configurations;

internal class ClothingConfiguration : IEntityTypeConfiguration<Clothing>
{
    public void Configure(EntityTypeBuilder<Clothing> builder)
    {
        builder.HasKey(clothing => clothing.Id);

        builder.Property(clothing => clothing.Type).HasConversion<string>();

        builder
            .HasMany(clothing => clothing.Images)
            .WithOne(image => image.Clothing).HasForeignKey("ClothingId");

        builder.HasMany(clothing => clothing.Outfits)
            .WithMany(outfit => outfit.Clothing);
    }
}