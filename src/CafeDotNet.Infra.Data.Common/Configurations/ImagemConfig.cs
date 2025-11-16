using CafeDotNet.Core.Galery.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeDotNet.Infra.Data.Common.Configurations;

public class ImageConfig : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Imagens");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(Image.FileNameMaxLength);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(Image.ContentTypeMaxLength);

        builder.Property(x => x.Size)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Image.DescriptionMaxLength);

        builder.HasIndex(x => x.FileName).IsUnique();
    }
}
