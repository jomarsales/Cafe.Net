using CafeDotNet.Infra.Data.Common.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CafeDotNet.Infra.Data.PostgreSql.Extensions
{
    public static class PostgresServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgresDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CafeDbContext>(options => options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("CafeDotNet.Infra.Data.PostgreSql")));
            
            return services;
        }

        public static DbContextOptionsBuilder<CafeDbContext> UsePostgresDatabase(this DbContextOptionsBuilder<CafeDbContext> optionsBuilder, string connectionString)
        {
            return optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("CafeDotNet.Infra.Data.PostgreSql"));
        }
    }
}
