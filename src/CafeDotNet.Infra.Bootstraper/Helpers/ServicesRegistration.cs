using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Core.Users.Services;
using CafeDotNet.Manager.Users.Interfaces;
using CafeDotNet.Manager.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CafeDotNet.Infra.Bootstraper.Helpers
{
    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
