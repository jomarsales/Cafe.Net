using CafeDotNet.Core.Articles.Entities;
using CafeDotNet.Core.Base.Entities;
using CafeDotNet.Core.Galery.Entities;
using CafeDotNet.Core.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CafeDotNet.Infra.Data.Common.Context;

public class CafeDbContext : DbContext
{
    public CafeDbContext(DbContextOptions<CafeDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyEntityBaseConfiguration(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CafeDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var serviceProvider = optionsBuilder.Options.GetExtension<CoreOptionsExtension>()?.ApplicationServiceProvider;
       
        var logger = serviceProvider?.GetService<ILogger<CafeDbContext>>();

        if (logger != null)
        {
            optionsBuilder.LogTo(log => logger.LogInformation(log), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information).EnableSensitiveDataLogging();
        }
    }

    private void ApplyEntityBaseConfiguration(ModelBuilder modelBuilder)
    {
        var entityBaseType = typeof(EntityBase);

        var entityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(t => entityBaseType.IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            var builder = modelBuilder.Entity(entityType.ClrType);

            builder.Property(nameof(EntityBase.CreatedAt)).IsRequired();
            builder.Property(nameof(EntityBase.UpdatedAt));
            builder.Property(nameof(EntityBase.IsActive));
        }
    }
}