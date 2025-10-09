using CafeDotNet.Core.Articles.Entities;
using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CafeDotNet.Infra.Data.Common.Context;

public class CafeDbContext : DbContext
{
    public CafeDbContext(DbContextOptions<CafeDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CafeDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        var serviceProvider = optionsBuilder.Options.GetExtension<CoreOptionsExtension>()?.ApplicationServiceProvider;
       
        var logger = serviceProvider?.GetService<ILogger<CafeDbContext>>();

        if (logger != null)
        {
            optionsBuilder.LogTo(log => logger.LogInformation(log), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information).EnableSensitiveDataLogging();
        }
    }
}