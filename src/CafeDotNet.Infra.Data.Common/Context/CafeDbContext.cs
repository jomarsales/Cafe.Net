using Microsoft.EntityFrameworkCore;

namespace CafeDotNet.Infra.Data.Common.Context;

public class CafeDbContext : DbContext
{
    public CafeDbContext(DbContextOptions<CafeDbContext> options)
        : base(options) { }

    //public DbSet<Article> Articles { get; set; }
    //public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(CafeDbContext).Assembly);
    }
}