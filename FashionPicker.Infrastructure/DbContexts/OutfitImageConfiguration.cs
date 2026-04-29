using FashionPicker.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionPicker.Infrastructure.DbContexts;

public class OutfitImageConfiguration : IEntityTypeConfiguration<OutfitImage>
{
    public void Configure(EntityTypeBuilder<OutfitImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Url).HasMaxLength(500).IsRequired();
    }
}