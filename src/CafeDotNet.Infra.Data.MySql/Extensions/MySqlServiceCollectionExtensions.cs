using CafeDotNet.Infra.Data.Common.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CafeDotNet.Infra.Data.MySql.Extensions;

public static class MySqlServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CafeDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), sql => sql.MigrationsAssembly("CafeDotNet.Infra.Data.MySql")));
       
        return services;
    }
}