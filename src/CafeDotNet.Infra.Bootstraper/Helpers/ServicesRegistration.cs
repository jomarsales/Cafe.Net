using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Galery.Interfaces;
using CafeDotNet.Core.Galery.Services;
using CafeDotNet.Core.Gallery.Interfaces;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Core.Users.Services;
using CafeDotNet.Infra.Bootstraper.Services;
using CafeDotNet.Infra.Data.Common.Configurations;
using CafeDotNet.Infra.Data.Common.Interfaces;
using CafeDotNet.Infra.Data.Common.Services;
using CafeDotNet.Manager.Galery.Interfaces;
using CafeDotNet.Manager.Galery.Services;
using CafeDotNet.Manager.Users.Interfaces;
using CafeDotNet.Manager.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CafeDotNet.Infra.Bootstraper.Helpers
{
    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IGaleryManager, GaleryManager>();
            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IImageStorage, DiskImageStorage>();

            return services;
        }

        public static IServiceCollection RegisterAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
