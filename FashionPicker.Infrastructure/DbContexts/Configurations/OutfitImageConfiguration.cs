using FashionPicker.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionPicker.Infrastructure.DbContexts.Configurations;

public class OutfitImageConfiguration : IEntityTypeConfiguration<OutfitImage>
{
    public void Configure(EntityTypeBuilder<OutfitImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SmallSizeUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.OriginalSizeUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.MimeType).IsRequired();
        builder.Property(x => x.MediumSizeUrl).HasMaxLength(500);
        builder.Property(x => x.BigSizeUrl).HasMaxLength(500);
    }
}