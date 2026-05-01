using FashionPicker.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionPicker.Infrastructure.DbContexts.Configurations;

public class ClothingImageConfiguration: IEntityTypeConfiguration<ClothingImage>
{
    public void Configure(EntityTypeBuilder<ClothingImage> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(x => x.SmallSizeUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.OriginalSizeUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.MimeType).IsRequired();
        builder.Property(x => x.MediumSizeUrl).HasMaxLength(500);
        builder.Property(x => x.BigSizeUrl).HasMaxLength(500);
    }
}