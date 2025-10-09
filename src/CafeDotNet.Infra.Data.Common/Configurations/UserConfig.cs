using CafeDotNet.Core.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeDotNet.Infra.Data.Common.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users"); 
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(u => u.Password, pw =>
        {
            pw.Property(p => p.Salt).HasMaxLength(50).IsRequired();
            pw.Property(p => p.Hash).HasMaxLength(256).IsRequired();
            pw.ToTable("Users");
        });

        builder.Property(x => x.Role).HasConversion<int>().IsRequired();

        //Base
        builder.Property(a => a.CreatedAt).IsRequired();
        builder.Property(a => a.UpdatedAt);
        builder.Property(a => a.IsActive);

        builder.HasIndex(x => x.Username).IsUnique(); 
    }
}
