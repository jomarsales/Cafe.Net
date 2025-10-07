using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CafeDotNet.Infra.Mail.DTOs;

namespace CafeDotNet.Infra.Mail.Helpers
{
    public static class EmailConfiguration
    {
        public static IServiceCollection AddCustomEmailSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            
            return services;
        }
    }
}
