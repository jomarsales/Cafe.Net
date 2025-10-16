using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CafeDotNet.Infra.Data.Common.DTOs;
using CafeDotNet.Infra.Data.MySql.Extensions;
using CafeDotNet.Infra.Data.PostgreSql.Extensions;
using CafeDotNet.Infra.Data.SqlServer.Extensions;

namespace CafeDotNet.Infra.Bootstraper.Helpers
{
    public static class DataBaseRegistration
    {
        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));

            using var provider = services.BuildServiceProvider();
            var dbSettings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            if (dbSettings is null)
            {
                throw new ArgumentNullException(nameof(dbSettings), "Configuração 'DatabaseSettings' não encontrada no appsettings.json.");
            }

            if (string.IsNullOrWhiteSpace(dbSettings.Provider))
            {
                throw new ArgumentException("O provider do banco de dados não foi configurado.");
            }

            switch (dbSettings.Provider)
            {
                case "SqlServer":
                    services.AddSqlServerDatabase(dbSettings.ConnectionStrings.SqlServer);
                    break;

                case "PostgreSql":
                    services.AddPostgresDatabase(dbSettings.ConnectionStrings.Postgres);
                    break;

                case "MySql":
                    services.AddMySqlDatabase(dbSettings.ConnectionStrings.MySql);
                    break;

                default:
                    throw new NotSupportedException($"Provider '{provider}' não é suportado.");
            }

            return services;
        }

        public static IServiceCollection RegisterDatabaseServices(this IServiceCollection services)
        {
            //Repositories, etc

            return services;
        }
    }
}
