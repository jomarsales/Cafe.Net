using CafeDotNet.Infra.Data.Common.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CafeDotNet.Infra.Data.SqlServer.Extensions;

public static class SqlServerServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CafeDbContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("CafeDotNet.Infra.Data.SqlServer")));

        return services;
    }
}