using CafeDotNet.Infra.Data.Common.Context;
using CafeDotNet.Infra.Data.Common.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CafeDotNet.Infra.Data.SqlServer.Extensions;
using CafeDotNet.Infra.Data.PostgreSql.Extensions;
using CafeDotNet.Infra.Data.MySql.Extensions;


namespace CafeDotNet.Web;

public class CafeDotNetDbContextFactory : IDesignTimeDbContextFactory<CafeDbContext>
{
    public CafeDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .Build();

        var dbSettings = new DatabaseSettings();
        configuration.GetSection("Database").Bind(dbSettings);

        var optionsBuilder = new DbContextOptionsBuilder<CafeDbContext>();

        if (dbSettings is null || string.IsNullOrWhiteSpace(dbSettings.Provider))
        {
            throw new NotSupportedException("Configuração de provedor de banco de dados inválida.");
        }

        switch (dbSettings.Provider)
        {
            case "SqlServer":
                optionsBuilder.UseSqlServerDatabase(dbSettings.ConnectionStrings.SqlServer);
                break;

            case "PostgreSql":
                optionsBuilder.UsePostgresDatabase(dbSettings.ConnectionStrings.Postgres);
                break;

            case "MySql":
                optionsBuilder.UseMySqlDatabase(dbSettings.ConnectionStrings.MySql);
                break;

            default:
                throw new NotSupportedException($"Provider '{dbSettings.Provider}' não é suportado para design-time.");
        }

        return new CafeDbContext(optionsBuilder.Options);
    }
}
