﻿using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Core.Users.Services;
using CafeDotNet.Infra.Bootstraper.Interfaces;
using CafeDotNet.Infra.Bootstraper.Services;
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

            return services;
        }

        public static IServiceCollection RegisterAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
