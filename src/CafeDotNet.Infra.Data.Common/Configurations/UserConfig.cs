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

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(200);

        //builder.Property(x => x.Papel)
        //    .IsRequired()
        //    .HasMaxLength(20)
        //    .HasDefaultValue("padrao"); 

        builder.HasIndex(x => x.Username).IsUnique(); 
    }
}
