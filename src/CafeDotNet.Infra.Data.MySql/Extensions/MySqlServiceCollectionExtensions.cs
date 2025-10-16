using CafeDotNet.Infra.Data.Common.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CafeDotNet.Infra.Data.MySql.Extensions;

public static class MySqlServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CafeDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), sql => sql.MigrationsAssembly("CafeDotNet.Infra.Data.MySql")));
       
        return services;
    }

    public static DbContextOptionsBuilder<CafeDbContext> UseMySqlDatabase(this DbContextOptionsBuilder<CafeDbContext> optionsBuilder, string connectionString)
    {
        return optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("CafeDotNet.Infra.Data.MySql"));
    }
}