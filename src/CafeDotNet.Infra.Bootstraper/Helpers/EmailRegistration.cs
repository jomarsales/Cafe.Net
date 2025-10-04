using CafeDotNet.Infra.Mail.Interfaces;
using CafeDotNet.Infra.Mail.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CafeDotNet.Infra.Bootstraper.Helpers
{
    public static class EmailRegistration
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            
            return services;
        }
    }
}
