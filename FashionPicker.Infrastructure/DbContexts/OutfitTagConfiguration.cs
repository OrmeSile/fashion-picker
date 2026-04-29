using FashionPicker.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionPicker.Infrastructure.DbContexts;

internal class OutfitTagConfiguration : IEntityTypeConfiguration<OutfitTag>
{
    public void Configure(EntityTypeBuilder<OutfitTag> builder)
    {
        builder.HasKey(outfitTag => outfitTag.Id);

        builder.Property(outfitTag => outfitTag.Value).HasMaxLength(100).IsRequired();

        builder.HasIndex(x => x.Value).IsUnique();
    }
}