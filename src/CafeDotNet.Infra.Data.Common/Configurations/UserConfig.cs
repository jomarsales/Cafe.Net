using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.ValueObjects;
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
            .HasMaxLength(User.UsernameMaxLength);

        builder.OwnsOne(u => u.Password, pw =>
        {
            pw.Property(p => p.Salt).HasMaxLength(Password.SaltMaxLength).IsRequired();
            pw.Property(p => p.Hash).HasMaxLength(Password.HashMaxLength).IsRequired();
            pw.ToTable("Users");
        });

        builder.Property(x => x.Role).HasConversion<int>().IsRequired();

        builder.HasIndex(x => x.Username).IsUnique(); 
    }
}
